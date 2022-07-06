using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Authorize]
public class FlashSalePlanAppService :
    MultiStoreCrudAppService<FlashSalePlan, FlashSalePlanDto, Guid, FlashSalePlanGetListInput, FlashSalePlanCreateDto, FlashSalePlanUpdateDto>,
    IFlashSalePlanAppService
{
    protected override string CrossStorePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.CrossStore;
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }
    protected override string CreatePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Create;
    protected override string UpdatePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Update;
    protected override string DeletePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Delete;

    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }

    protected IProductAppService ProductAppService { get; }

    protected IDistributedCache TokenDistributedCache { get; }

    protected IDistributedCache<FlashSalePlanCacheItem, Guid> DistributedCache { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    protected IAbpDistributedLock DistributedLock { get; }

    protected IFlashSalePlanHasher FlashSalesPlanHasher { get; }

    public FlashSalePlanAppService(
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService,
        IDistributedCache tokenDistributedCache,
        IDistributedCache<FlashSalePlanCacheItem, Guid> distributedCache,
        IDistributedEventBus distributedEventBus,
        IFlashSaleResultRepository flashSaleResultRepository,
        IAbpDistributedLock distributedLock,
        IFlashSalePlanHasher flashSalesPlanHasher)
        : base(flashSalePlanRepository)
    {
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
        TokenDistributedCache = tokenDistributedCache;
        DistributedCache = distributedCache;
        DistributedEventBus = distributedEventBus;
        FlashSaleResultRepository = flashSaleResultRepository;
        DistributedLock = distributedLock;
        FlashSalesPlanHasher = flashSalesPlanHasher;
    }

    public override async Task<FlashSalePlanDto> GetAsync(Guid id)
    {
        var flashSalePlan = await GetEntityByIdAsync(id);

        await CheckMultiStorePolicyAsync(flashSalePlan.StoreId, GetPolicyName);

        if (!flashSalePlan.IsPublished)
        {
            await CheckMultiStorePolicyAsync(flashSalePlan.StoreId, FlashSalesPermissions.FlashSalePlan.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalePlan);
    }

    public override async Task<PagedResultDto<FlashSalePlanDto>> GetListAsync(FlashSalePlanGetListInput input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);

        return await base.GetListAsync(input);
    }

    public override async Task<FlashSalePlanDto> CreateAsync(FlashSalePlanCreateDto input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

        var product = await ProductAppService.GetAsync(input.ProductId);
        var productSku = product.GetSkuById(input.ProductSkuId);

        await ValidateCreateOrUpdateProductAsync(input.ProductId, product, input.ProductSkuId, productSku, input.StoreId);

        var flashSalePlan = new FlashSalePlan(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            input.StoreId,
            input.BeginTime,
            input.EndTime,
            input.ProductId,
            input.ProductSkuId,
            input.IsPublished
        );

        await FlashSalePlanRepository.InsertAsync(flashSalePlan, autoSave: true);

        return await MapToGetOutputDtoAsync(flashSalePlan);
    }

    public override async Task<FlashSalePlanDto> UpdateAsync(Guid id, FlashSalePlanUpdateDto input)
    {
        var flashSalePlan = await GetEntityByIdAsync(id);
        var product = await ProductAppService.GetAsync(input.ProductId);
        var productSku = product.GetSkuById(input.ProductSkuId);

        await CheckMultiStorePolicyAsync(product.StoreId, UpdatePolicyName);

        await ValidateCreateOrUpdateProductAsync(input.ProductId, product, input.ProductSkuId, productSku, flashSalePlan.StoreId);

        if (await ExistRelatedFlashSalesResultsAsync(id) && (input.ProductId != flashSalePlan.ProductId || input.ProductSkuId != flashSalePlan.ProductSkuId))
        {
            throw new RelatedFlashSaleResultsExistException(id);
        }

        flashSalePlan.SetTimeRange(input.BeginTime, input.EndTime);
        flashSalePlan.SetProductSku(flashSalePlan.StoreId, input.ProductId, input.ProductSkuId);
        flashSalePlan.SetPublished(input.IsPublished);

        flashSalePlan.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await FlashSalePlanRepository.UpdateAsync(flashSalePlan, autoSave: true);

        return await MapToGetOutputDtoAsync(flashSalePlan);
    }

    public override async Task DeleteAsync(Guid id)
    {
        var flashSalePlan = await GetEntityByIdAsync(id);

        await CheckMultiStorePolicyAsync(flashSalePlan.StoreId, DeletePolicyName);

        if (await ExistRelatedFlashSalesResultsAsync(id))
        {
            throw new RelatedFlashSaleResultsExistException(id);
        }

        await FlashSalePlanRepository.DeleteAsync(flashSalePlan);
    }

    protected override async Task<IQueryable<FlashSalePlan>> CreateFilteredQueryAsync(FlashSalePlanGetListInput input)
    {
        if (input.IncludeUnpublished)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, FlashSalesPermissions.FlashSalePlan.Manage);
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
        var plan = await GetFlashSalePlanCacheAsync(id);
        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        await ValidatePreOrderAsync(plan, product, productSku);

        await SetCacheHashTokenAsync(plan, product, productSku);
    }

    public virtual async Task CheckPreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalePlanCacheAsync(id);

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

        await ValidatePreOrderAsync(plan, product, productSku);
    }

    public virtual async Task CreateOrderAsync(Guid id, CreateOrderInput input)
    {
        var plan = await GetFlashSalePlanCacheAsync(id);
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

    protected virtual Task<FlashSaleReduceInventoryEto> PrepareFlashSalesReduceInventoryEtoAsync(
        FlashSalePlanCacheItem plan,
        Guid resultId,
        CreateOrderInput input,
        Guid userId,
        DateTime now,
        string hashToken)
    {
        var planEto = ObjectMapper.Map<FlashSalePlanCacheItem, FlashSalePlanEto>(plan);
        planEto.TenantId = CurrentTenant.Id;

        var eto = new FlashSaleReduceInventoryEto()
        {
            TenantId = CurrentTenant.Id,
            PlanId = plan.Id,
            UserId = userId,
            PendingResultId = resultId,
            StoreId = plan.StoreId,
            CreateTime = now,
            CustomerRemark = input.CustomerRemark,
            Plan = planEto,
            HashToken = hashToken
        };

        foreach (var item in input.ExtraProperties)
        {
            eto.ExtraProperties.Add(item.Key, item.Value);
        }

        return Task.FromResult(eto);
    }

    protected virtual async Task<FlashSalePlanCacheItem> GetFlashSalePlanCacheAsync(Guid id)
    {
        return await DistributedCache.GetOrAddAsync(id, async () =>
        {
            var flashSalesPlan = await FlashSalePlanRepository.GetAsync(id);
            return ObjectMapper.Map<FlashSalePlan, FlashSalePlanCacheItem>(flashSalesPlan);
        });
    }

    protected virtual Task<string> GetCacheKeyAsync(FlashSalePlanCacheItem plan)
    {
        return Task.FromResult($"eshopflashsales_{CurrentTenant.Id}_{CurrentUser.Id}_{plan.ProductSkuId}");
    }

    protected virtual async Task<string> GetCacheHashTokenAsync(FlashSalePlanCacheItem plan)
    {
        return await TokenDistributedCache.GetStringAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task RemoveCacheHashTokenAsync(FlashSalePlanCacheItem plan)
    {
        await TokenDistributedCache.RemoveAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task SetCacheHashTokenAsync(FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        var hashToken = await FlashSalesPlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        await TokenDistributedCache.SetStringAsync(await GetCacheKeyAsync(plan), hashToken, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        });
    }

    protected virtual async Task<bool> CompareHashTokenAsync(string cacheHashToken, FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            return false;
        }

        var hashToken = await FlashSalesPlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        return cacheHashToken == hashToken;
    }

    protected virtual async Task<FlashSaleResult> CreatePendingFlashSalesResultAsync(FlashSalePlanCacheItem plan, Guid userId)
    {
        var lockKey = $"create-flash-sales-order-{plan.Id}-{userId}";

        await using var handle = await DistributedLock.TryAcquireAsync(lockKey);

        if (handle == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.BusyToCreateFlashSaleOrder);
        }

        // Prevent repeat submit
        if (await FlashSaleResultRepository.AnyAsync(x =>
            x.PlanId == plan.Id && x.UserId == userId && (x.Status == FlashSaleResultStatus.Successful || x.Status == FlashSaleResultStatus.Pending))
            )
        {
            throw new BusinessException(FlashSalesErrorCodes.DuplicateFlashSalesOrder);
        }

        var result = new FlashSaleResult(
            id: GuidGenerator.Create(),
            tenantId: CurrentTenant.Id,
            storeId: plan.StoreId,
            planId: plan.Id,
            status: FlashSaleResultStatus.Pending,
            reason: null,
            userId: userId,
            orderId: null
        );
        return await FlashSaleResultRepository.InsertAsync(result, autoSave: true);
    }

    protected virtual Task ValidatePreOrderAsync(FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
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
            throw new EntityNotFoundException(typeof(FlashSalePlan), plan.Id);
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

    protected virtual async Task<bool> ExistRelatedFlashSalesResultsAsync(Guid planId)
    {
        return await FlashSaleResultRepository.AnyAsync(x => x.PlanId == planId);
    }

    protected virtual Task ValidateCreateOrUpdateProductAsync(Guid productId, ProductDto product, Guid productSkuId, ProductSkuDto productSku, Guid storeId)
    {
        if (product.StoreId != storeId)
        {
            throw new ProductIsNotInThisStoreException(productId, storeId);
        }

        if (productSku == null)
        {
            throw new ProductSkuIsNotFoundException(productSkuId);
        }

        if (product.InventoryStrategy != InventoryStrategy.FlashSales)
        {
            throw new UnexpectedInventoryStrategyException(InventoryStrategy.FlashSales);
        }

        return Task.CompletedTask;
    }
}
