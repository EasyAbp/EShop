using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Authorize]
public class FlashSalesPlanAppService :
    MultiStoreCrudAppService<FlashSalesPlan, FlashSalesPlanDto, Guid, FlashSalesPlanGetListInput, FlashSalesPlanCreateDto, FlashSalesPlanUpdateDto>,
    IFlashSalesPlanAppService
{
    protected override string CrossStorePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.CrossStore;
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }
    protected override string CreatePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Create;
    protected override string UpdatePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Update;
    protected override string DeletePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Delete;

    protected IFlashSalesPlanRepository FlashSalesPlanRepository { get; }

    protected IProductAppService ProductAppService { get; }

    protected IDistributedCache TokenDistributedCache { get; }

    protected IDistributedCache<FlashSalesPlanCacheItem, Guid> DistributedCache { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IFlashSalesResultRepository FlashSalesResultRepository { get; }

    protected IAbpDistributedLock DistributedLock { get; }

    protected IFlashSalesPlanHasher FlashSalesPlanHasher { get; }

    public FlashSalesPlanAppService(
        IFlashSalesPlanRepository flashSalesPlanRepository,
        IProductAppService productAppService,
        IDistributedCache tokenDistributedCache,
        IDistributedCache<FlashSalesPlanCacheItem, Guid> distributedCache,
        IDistributedEventBus distributedEventBus,
        IFlashSalesResultRepository flashSalesResultRepository,
        IAbpDistributedLock distributedLock,
        IFlashSalesPlanHasher flashSalesPlanHasher)
        : base(flashSalesPlanRepository)
    {
        FlashSalesPlanRepository = flashSalesPlanRepository;
        ProductAppService = productAppService;
        TokenDistributedCache = tokenDistributedCache;
        DistributedCache = distributedCache;
        DistributedEventBus = distributedEventBus;
        FlashSalesResultRepository = flashSalesResultRepository;
        DistributedLock = distributedLock;
        FlashSalesPlanHasher = flashSalesPlanHasher;
    }

    public override async Task<FlashSalesPlanDto> GetAsync(Guid id)
    {
        var flashSalesPlan = await GetEntityByIdAsync(id);

        await CheckMultiStorePolicyAsync(flashSalesPlan.StoreId, GetPolicyName);

        if (!flashSalesPlan.IsPublished)
        {
            await CheckMultiStorePolicyAsync(flashSalesPlan.StoreId, FlashSalesPermissions.FlashSalesPlan.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    public override async Task<FlashSalesPlanDto> CreateAsync(FlashSalesPlanCreateDto input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

        var product = await ProductAppService.GetAsync(input.ProductId);
        product.GetSkuById(input.ProductSkuId);

        if (product.StoreId != input.StoreId)
        {
            throw new ProductIsNotInThisStoreException(input.ProductId, input.StoreId);
        }

        var flashSalesPlan = new FlashSalesPlan(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            input.StoreId,
            input.BeginTime,
            input.EndTime,
            input.ProductId,
            input.ProductSkuId,
            input.IsPublished
        );

        await FlashSalesPlanRepository.InsertAsync(flashSalesPlan, autoSave: true);

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    public override async Task<FlashSalesPlanDto> UpdateAsync(Guid id, FlashSalesPlanUpdateDto input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, UpdatePolicyName);

        var product = await ProductAppService.GetAsync(input.ProductId);
        product.GetSkuById(input.ProductSkuId);

        if (product.StoreId != input.StoreId)
        {
            throw new ProductIsNotInThisStoreException(input.ProductId, input.StoreId);
        }

        var flashSalesPlan = await GetEntityByIdAsync(id);

        flashSalesPlan.SetTimeRange(input.BeginTime, input.EndTime);
        flashSalesPlan.SetProductSku(input.StoreId, input.ProductId, input.ProductSkuId);
        flashSalesPlan.SetPublished(input.IsPublished);

        flashSalesPlan.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await FlashSalesPlanRepository.UpdateAsync(flashSalesPlan, autoSave: true);

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    public override async Task DeleteAsync(Guid id)
    {
        var flashSalesPlan = await GetEntityByIdAsync(id);

        await CheckMultiStorePolicyAsync(flashSalesPlan.StoreId, DeletePolicyName);

        await FlashSalesPlanRepository.DeleteAsync(flashSalesPlan);
    }

    protected override async Task<IQueryable<FlashSalesPlan>> CreateFilteredQueryAsync(FlashSalesPlanGetListInput input)
    {
        if (input.IncludeUnpublished)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, FlashSalesPermissions.FlashSalesPlan.Manage);
        }

        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.ProductId.HasValue, x => x.ProductId == input.ProductId.Value)
            .WhereIf(input.ProductSkuId.HasValue, x => x.ProductSkuId == input.ProductSkuId.Value)
            .WhereIf(!input.IncludeUnpublished, x => x.IsPublished)
            .WhereIf(input.Start.HasValue, x => x.BeginTime >= input.Start.Value)
            .WhereIf(input.End.HasValue, x => x.BeginTime <= input.End.Value);
    }

    public virtual async Task PreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);
        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        await CheckPreOrderAsync(plan, product, productSku);

        await SetCacheHashTokenAsync(plan, product, productSku);
    }

    public virtual async Task CheckPreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);

        var cacheHashToken = await GetCacheHashTokenAsync(plan);
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExpired);
        }

        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        if (!await CompareHashTokenAsync(cacheHashToken, plan, product, productSku))
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExpired);
        }

        await CheckPreOrderAsync(plan, product, productSku);
    }

    public virtual async Task CreateOrderAsync(Guid id, CreateOrderInput input)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);
        var now = Clock.Now;
        if (plan.BeginTime > now)
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductIsNotPublished);
        }

        if (now >= plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSaleIsOver);
        }

        var cacheHashToken = await GetCacheHashTokenAsync(plan);
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExpired);
        }

        await RemoveCacheHashTokenAsync(plan);

        var userId = CurrentUser.GetId();

        var result = await CreatePendingFlashSalesResultAsync(plan, userId);

        var flashSalesReduceInventoryEto = await PrepareFlashSalesReduceInventoryEtoAsync(plan, result.Id, input, userId, now, cacheHashToken);

        /*
         * FlashSalesReduceInventoryEto(success) -> CreateFlashSalesOrderEto -> CreateFlashSalesOrderCompleteEto
         * FlashSalesReduceInventoryEto(failed) -> CreateFlashSalesOrderCompleteEto
         */
        await DistributedEventBus.PublishAsync(flashSalesReduceInventoryEto);
    }

    protected virtual Task<FlashSalesReduceInventoryEto> PrepareFlashSalesReduceInventoryEtoAsync(
        FlashSalesPlanCacheItem plan,
        Guid resultId,
        CreateOrderInput input,
        Guid userId,
        DateTime now,
        string hashToken)
    {
        var planEto = ObjectMapper.Map<FlashSalesPlanCacheItem, FlashSalesPlanEto>(plan);
        planEto.TenantId = CurrentTenant.Id;

        var eto = new FlashSalesReduceInventoryEto()
        {
            TenantId = CurrentTenant.Id,
            PlanId = plan.Id,
            UserId = userId,
            PendingResultId = resultId,
            StoreId = plan.StoreId,
            CreateTime = now,
            CustomerRemark = input.CustomerRemark,
            Quantity = 1,//should configure
            Plan = planEto,
            HashToken = hashToken
        };

        foreach (var item in input.ExtraProperties)
        {
            eto.ExtraProperties.Add(item.Key, item.Value);
        }

        return Task.FromResult(eto);
    }

    protected virtual async Task<FlashSalesPlanCacheItem> GetFlashSalesPlanCacheAsync(Guid id)
    {
        return await DistributedCache.GetOrAddAsync(id, async () =>
        {
            var flashSalesPlan = await FlashSalesPlanRepository.GetAsync(id);
            return ObjectMapper.Map<FlashSalesPlan, FlashSalesPlanCacheItem>(flashSalesPlan);
        });
    }

    protected virtual Task<string> GetCacheKeyAsync(FlashSalesPlanCacheItem plan)
    {
        return Task.FromResult($"eshopflashsales_{CurrentTenant.Id}_{CurrentUser.Id}_{plan.ProductSkuId}");
    }

    protected virtual async Task<string> GetCacheHashTokenAsync(FlashSalesPlanCacheItem plan)
    {
        return await TokenDistributedCache.GetStringAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task RemoveCacheHashTokenAsync(FlashSalesPlanCacheItem plan)
    {
        await TokenDistributedCache.RemoveAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task SetCacheHashTokenAsync(FlashSalesPlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        var hashToken = await FlashSalesPlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        await TokenDistributedCache.SetStringAsync(await GetCacheKeyAsync(plan), hashToken, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        });
    }

    protected virtual async Task<bool> CompareHashTokenAsync(string cacheHashToken, FlashSalesPlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            return false;
        }

        var hashToken = await FlashSalesPlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        return cacheHashToken == hashToken;
    }

    protected virtual async Task<FlashSalesResult> CreatePendingFlashSalesResultAsync(FlashSalesPlanCacheItem plan, Guid userId)
    {
        var lockKey = $"create-flash-sales-order-{plan.Id}-{userId}";

        await using var handle = await DistributedLock.TryAcquireAsync(lockKey);

        if (handle == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.BusyToCreateFlashSaleOrder);
        }

        // Prevent repeat submit
        if (await FlashSalesResultRepository.AnyAsync(x =>
            x.PlanId == plan.Id && x.UserId == userId && (x.Status == FlashSalesResultStatus.Successful || x.Status == FlashSalesResultStatus.Pending))
            )
        {
            throw new BusinessException(FlashSalesErrorCodes.DuplicateFlashSalesOrder);
        }

        var result = new FlashSalesResult(
            id: GuidGenerator.Create(),
            tenantId: CurrentTenant.Id,
            storeId: plan.StoreId,
            planId: plan.Id,
            status: FlashSalesResultStatus.Pending,
            reason: null,
            userId: userId,
            orderId: null
        );
        return await FlashSalesResultRepository.InsertAsync(result, autoSave: true);
    }

    protected virtual Task CheckPreOrderAsync(FlashSalesPlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        if (!product.IsPublished)
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductIsNotPublished);
        }

        if (product.InventoryStrategy != InventoryStrategy.FlashSales)
        {
            throw new UnexpectedInventoryStrategyException(InventoryStrategy.FlashSales);
        }

        if (!plan.IsPublished)
        {
            throw new EntityNotFoundException(typeof(FlashSalesPlan), plan.Id);
        }

        if (Clock.Now >= plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSaleIsOver);
        }

        if (productSku.Inventory < 1)
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductSkuInventoryExceeded);
        }

        return Task.CompletedTask;
    }
}
