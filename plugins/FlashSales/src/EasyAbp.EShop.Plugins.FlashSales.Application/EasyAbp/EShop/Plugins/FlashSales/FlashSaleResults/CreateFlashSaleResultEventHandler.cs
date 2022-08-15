using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class CreateFlashSaleResultEventHandler : IDistributedEventHandler<CreateFlashSaleResultEto>,
    ITransientDependency
{
    protected IObjectMapper ObjectMapper { get; }
    protected IGuidGenerator GuidGenerator { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected ILogger<CreateFlashSaleResultEventHandler> Logger { get; }
    protected IAbpDistributedLock AbpDistributedLock { get; }
    protected IDistributedEventBus DistributedEventBus { get; }
    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public CreateFlashSaleResultEventHandler(
        IObjectMapper objectMapper,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        ILogger<CreateFlashSaleResultEventHandler> logger,
        IAbpDistributedLock abpDistributedLock,
        IDistributedEventBus distributedEventBus,
        IFlashSaleCurrentResultCache flashSaleCurrentResultCache,
        IFlashSaleResultRepository flashSaleResultRepository)
    {
        ObjectMapper = objectMapper;
        GuidGenerator = guidGenerator;
        CurrentTenant = currentTenant;
        Logger = logger;
        AbpDistributedLock = abpDistributedLock;
        DistributedEventBus = distributedEventBus;
        FlashSaleCurrentResultCache = flashSaleCurrentResultCache;
        FlashSaleResultRepository = flashSaleResultRepository;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateFlashSaleResultEto eventData)
    {
        await using var handle = await AbpDistributedLock.TryAcquireAsync(await GetLockKeyAsync(eventData));

        if (handle is null)
        {
            throw new AbpException("Concurrent flash sale result creation");
        }

        var ongoingResult = await FlashSaleResultRepository.FirstOrDefaultAsync(x =>
            x.PlanId == eventData.Plan.Id &&
            x.UserId == eventData.UserId &&
            x.Status != FlashSaleResultStatus.Failed);

        if (ongoingResult is not null)
        {
            Logger.LogWarning("Duplicate ongoing flash sale result creation");

            await FlashSaleCurrentResultCache.SetAsync(eventData.Plan.Id, eventData.UserId,
                new FlashSaleCurrentResultCacheItem
                {
                    TenantId = ongoingResult.TenantId,
                    ResultDto = ObjectMapper.Map<FlashSaleResult, FlashSaleResultDto>(ongoingResult)
                });

            return;
        }

        var result = new FlashSaleResult(
            id: eventData.ResultId,
            tenantId: CurrentTenant.Id,
            storeId: eventData.Plan.StoreId,
            planId: eventData.Plan.Id,
            userId: eventData.UserId,
            reducedInventoryTime: eventData.ReducedInventoryTime
        );

        var eto = new CreateFlashSaleOrderEto
        {
            TenantId = eventData.TenantId,
            ResultId = eventData.ResultId,
            UserId = eventData.UserId,
            CustomerRemark = eventData.CustomerRemark,
            Plan = eventData.Plan,
            HashToken = eventData.HashToken
        };

        eventData.MapExtraPropertiesTo(eto, MappingPropertyDefinitionChecks.None);

        await DistributedEventBus.PublishAsync(eto);

        await FlashSaleResultRepository.InsertAsync(result, autoSave: true);
    }

    protected virtual Task<string> GetLockKeyAsync(CreateFlashSaleResultEto eventData)
    {
        return Task.FromResult($"eshopflashsales-creating-result_{eventData.Plan.Id}-{eventData.UserId}");
    }
}