using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCreationResultEventHandler : IDistributedEventHandler<FlashSaleOrderCreationResultEto>,
    ITransientDependency
{
    protected ILogger<FlashSaleOrderCreationResultEventHandler> Logger { get; }
    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }
    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }
    protected IProductAppService ProductAppService { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }
    protected IObjectMapper ObjectMapper { get; }
    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    public FlashSaleOrderCreationResultEventHandler(
        ILogger<FlashSaleOrderCreationResultEventHandler> logger,
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService,
        IUnitOfWorkManager unitOfWorkManager,
        IObjectMapper objectMapper,
        IFlashSaleCurrentResultCache flashSaleCurrentResultCache,
        IFlashSaleResultRepository flashSaleResultRepository)
    {
        Logger = logger;
        FlashSaleInventoryManager = flashSaleInventoryManager;
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
        UnitOfWorkManager = unitOfWorkManager;
        ObjectMapper = objectMapper;
        FlashSaleCurrentResultCache = flashSaleCurrentResultCache;
        FlashSaleResultRepository = flashSaleResultRepository;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(FlashSaleOrderCreationResultEto eventData)
    {
        var flashSaleResult = await FlashSaleResultRepository.GetAsync(eventData.ResultId);

        if (eventData.Success)
        {
            flashSaleResult.MarkAsSuccessful(eventData.OrderId!.Value);

            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);
        }
        else
        {
            var plan = await FlashSalePlanRepository.GetAsync(eventData.PlanId);
            var product = await ProductAppService.GetAsync(plan.ProductId);

            flashSaleResult.MarkAsFailed(eventData.Reason);

            await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);

            if (!await FlashSaleInventoryManager.TryRollBackInventoryAsync(
                    plan.TenantId, product.InventoryProviderName,
                    plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true
                ))
            {
                Logger.LogWarning("Try roll back inventory failed.");
                return; // Avoid to remove cache on the UOW completed.
            }
        }

        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            if (flashSaleResult.Status is FlashSaleResultStatus.Failed && eventData.AllowToTryAgain)
            {
                await FlashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
            }
            else
            {
                await FlashSaleCurrentResultCache.SetAsync(flashSaleResult.PlanId, flashSaleResult.UserId,
                    new FlashSaleCurrentResultCacheItem
                    {
                        TenantId = flashSaleResult.TenantId,
                        ResultDto = ObjectMapper.Map<FlashSaleResult, FlashSaleResultDto>(flashSaleResult)
                    });
            }
        });
    }
}