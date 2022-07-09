using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

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

    protected IDistributedCache<FlashSalePlanPreOrderCacheItem> PreOrderDistributedCache { get; }

    protected IDistributedCache<FlashSalePlanCacheItem, Guid> PlanDistributedCache { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    protected IAbpDistributedLock DistributedLock { get; }

    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }

    protected IDistributedCache DistributedCache { get; }

    protected FlashSalesOptions Options { get; }

    public FlashSalePlanAppService(
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService,
        IDistributedCache<FlashSalePlanPreOrderCacheItem> tokenDistributedCache,
        IDistributedCache<FlashSalePlanCacheItem, Guid> planDistributedCache,
        IDistributedEventBus distributedEventBus,
        IFlashSaleResultRepository flashSaleResultRepository,
        IAbpDistributedLock distributedLock,
        IFlashSalePlanHasher flashSalePlanHasher,
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IDistributedCache distributedCache,
        IOptionsMonitor<FlashSalesOptions> optionsMonitor)
        : base(flashSalePlanRepository)
    {
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
        PreOrderDistributedCache = tokenDistributedCache;
        PlanDistributedCache = planDistributedCache;
        DistributedEventBus = distributedEventBus;
        FlashSaleResultRepository = flashSaleResultRepository;
        DistributedLock = distributedLock;
        FlashSalePlanHasher = flashSalePlanHasher;
        FlashSaleInventoryManager = flashSaleInventoryManager;
        DistributedCache = distributedCache;
        Options = optionsMonitor.CurrentValue;
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

        if (await ExistRelatedFlashSaleResultsAsync(id) && (input.ProductId != flashSalePlan.ProductId || input.ProductSkuId != flashSalePlan.ProductSkuId))
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

        if (await ExistRelatedFlashSaleResultsAsync(id))
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

        await SetPreOrderCacheAsync(plan, product, productSku);
    }

    public virtual async Task CheckPreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalePlanCacheAsync(id);

        var preOrderCache = await GetPreOrderCacheAsync(plan);
        if (preOrderCache == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExpired);
        }

        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        if (!await CompareHashTokenAsync(preOrderCache.HashToken, plan, product, productSku))
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
            throw new BusinessException(FlashSalesErrorCodes.FlashSaleNotStarted);
        }

        if (now >= plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSaleIsOver);
        }

        var preOrderCache = await GetPreOrderCacheAsync(plan);
        if (preOrderCache == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExpired);
        }

        await RemovePreOrderCacheAsync(plan);

        var userId = CurrentUser.GetId();

        var lockKey = $"create-flash-sale-order-{plan.Id}-{userId}";

        await using var handle = await DistributedLock.TryAcquireAsync(lockKey);

        if (handle == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.BusyToCreateFlashSaleOrder);
        }

        var userFlashSaleResultCache = await GetUserFlashSaleResultCacheAsync(plan, userId);
        if (!userFlashSaleResultCache.IsNullOrWhiteSpace())
        {
            throw new BusinessException(FlashSalesErrorCodes.DuplicateFlashSalesOrder);
        }

        if (!await FlashSaleInventoryManager.TryIncreaseInventoryAsync(
            plan.TenantId, preOrderCache.InventoryProviderName,
            plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true))
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductSkuInventoryExceeded);
        }

        var result = await CreatePendingFlashSaleResultAsync(plan, userId, async (existsResultId) =>
        {
            await SetUserFlashSaleResultCacheAsync(plan, userId, existsResultId);

            await FlashSaleInventoryManager.TryIncreaseInventoryAsync(
                plan.TenantId, preOrderCache.InventoryProviderName,
                plan.StoreId, plan.ProductId, plan.ProductSkuId, 1, true
            );
        });

        await SetUserFlashSaleResultCacheAsync(plan, userId, result.Id);

        var createFlashSaleOrderEto = await PrepareCreateFlashSaleOrderEtoAsync(plan, result.Id, input, userId, now, preOrderCache.HashToken);

        await DistributedEventBus.PublishAsync(createFlashSaleOrderEto);
    }

    #region PreOrderCache

    protected virtual async Task<FlashSalePlanCacheItem> GetFlashSalePlanCacheAsync(Guid id)
    {
        return await PlanDistributedCache.GetOrAddAsync(id, async () =>
        {
            var flashSalePlan = await FlashSalePlanRepository.GetAsync(id);
            return ObjectMapper.Map<FlashSalePlan, FlashSalePlanCacheItem>(flashSalePlan);
        });
    }

    protected virtual Task<string> GetPreOrderCacheKeyAsync(FlashSalePlanCacheItem plan)
    {
        return Task.FromResult($"eshopflashsales_{CurrentTenant.Id}_{CurrentUser.Id}_{plan.ProductSkuId}");
    }

    protected virtual async Task<FlashSalePlanPreOrderCacheItem> GetPreOrderCacheAsync(FlashSalePlanCacheItem plan)
    {
        return await PreOrderDistributedCache.GetAsync(await GetPreOrderCacheKeyAsync(plan));
    }

    protected virtual async Task RemovePreOrderCacheAsync(FlashSalePlanCacheItem plan)
    {
        await PreOrderDistributedCache.RemoveAsync(await GetPreOrderCacheKeyAsync(plan));
    }

    protected virtual async Task SetPreOrderCacheAsync(FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        var hashToken = await FlashSalePlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        await PreOrderDistributedCache.SetAsync(await GetPreOrderCacheKeyAsync(plan), new FlashSalePlanPreOrderCacheItem()
        {
            HashToken = hashToken,
            PlanId = plan.Id,
            ProductId = product.Id,
            ProductSkuId = productSku.Id,
            InventoryProviderName = product.InventoryProviderName,
        }, new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = Options.PreOrderExpirationTime
        });
    }

    #endregion

    #region UserFlashSaleResultCache

    protected virtual Task<string> GetUserFlashSaleResultCacheKeyAsync(FlashSalePlanCacheItem plan, Guid userId)
    {
        return Task.FromResult($"flash-sale-result-{plan.Id}-{userId}");
    }

    protected virtual async Task<string> GetUserFlashSaleResultCacheAsync(FlashSalePlanCacheItem plan, Guid userId)
    {
        var userFlashSaleResultCacheKey = await GetUserFlashSaleResultCacheKeyAsync(plan, userId);
        return await DistributedCache.GetStringAsync(userFlashSaleResultCacheKey);
    }

    protected virtual async Task SetUserFlashSaleResultCacheAsync(FlashSalePlanCacheItem plan, Guid userId, Guid resultId)
    {
        var userFlashSaleResultCacheKey = await GetUserFlashSaleResultCacheKeyAsync(plan, userId);
        await DistributedCache.SetStringAsync(userFlashSaleResultCacheKey, resultId.ToString());
    }

    #endregion

    protected virtual async Task<bool> CompareHashTokenAsync(string originHashToken, FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku)
    {
        if (originHashToken.IsNullOrWhiteSpace())
        {
            return false;
        }

        var hashToken = await FlashSalePlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        return string.Equals(hashToken, originHashToken, StringComparison.InvariantCulture);
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

    protected virtual async Task<bool> ExistRelatedFlashSaleResultsAsync(Guid planId)
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

    protected virtual Task<CreateFlashSaleOrderEto> PrepareCreateFlashSaleOrderEtoAsync(
        FlashSalePlanCacheItem plan, Guid resultId, CreateOrderInput input,
        Guid userId, DateTime now, string hashToken)
    {
        var planEto = ObjectMapper.Map<FlashSalePlanCacheItem, FlashSalePlanEto>(plan);
        planEto.TenantId = CurrentTenant.Id;

        var eto = new CreateFlashSaleOrderEto()
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

    protected virtual async Task<FlashSaleResult> CreatePendingFlashSaleResultAsync(FlashSalePlanCacheItem plan, Guid userId, Func<Guid, Task> existResultPreProcess)
    {
        // Prevent repeat submit
        var existsResult = await FlashSaleResultRepository.FirstOrDefaultAsync(x => x.PlanId == plan.Id && x.UserId == userId);

        if (existsResult != null)
        {
            await existResultPreProcess(existsResult.Id);
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
}
