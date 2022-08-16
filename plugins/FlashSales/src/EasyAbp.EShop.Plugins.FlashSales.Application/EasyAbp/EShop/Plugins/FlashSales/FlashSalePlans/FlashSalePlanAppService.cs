using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Options;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSalePlanAppService :
    MultiStoreCrudAppService<FlashSalePlan, FlashSalePlanDto, Guid, FlashSalePlanGetListInput, FlashSalePlanCreateDto, FlashSalePlanUpdateDto>,
    IFlashSalePlanAppService
{
    /// <summary>
    /// The <see cref="GetPreOrderCacheKeyAsync(FlashSalePlanCacheItem)"/> cache key format.
    /// <para>{0}: FlashSalePlan ID</para>
    /// <para>{1}: User ID</para>
    /// </summary>
    public const string PreOrderCacheKeyFormat = "eshopflashsales_{0}_{1}";

    protected override string CrossStorePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.CrossStore;
    protected override string GetPolicyName { get; set; } = null;
    protected override string GetListPolicyName { get; set; } = null;
    protected override string CreatePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Create;
    protected override string UpdatePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Update;
    protected override string DeletePolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.Delete;
    protected virtual string PreOrderPolicyName { get; set; } = FlashSalesPermissions.FlashSalePlan.PreOrder;

    protected IFlashSalePlanRepository FlashSalePlanRepository { get; }

    protected IProductAppService ProductAppService { get; }

    protected IDistributedCache<FlashSalePlanPreOrderCacheItem> PreOrderDistributedCache { get; }

    protected IDistributedCache<FlashSalePlanCacheItem, Guid> PlanDistributedCache { get; }

    protected IDistributedCache<ProductCacheItem, Guid> ProductDistributedCache { get; }

    protected IDistributedEventBus DistributedEventBus { get; }

    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    protected IAbpDistributedLock DistributedLock { get; }

    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }

    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }

    protected FlashSalesOptions Options { get; }

    public FlashSalePlanAppService(
        IFlashSalePlanRepository flashSalePlanRepository,
        IProductAppService productAppService,
        IDistributedCache<FlashSalePlanPreOrderCacheItem> tokenDistributedCache,
        IDistributedCache<FlashSalePlanCacheItem, Guid> planDistributedCache,
        IDistributedCache<ProductCacheItem, Guid> productDistributedCache,
        IDistributedEventBus distributedEventBus,
        IFlashSaleResultRepository flashSaleResultRepository,
        IAbpDistributedLock distributedLock,
        IFlashSalePlanHasher flashSalePlanHasher,
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IFlashSaleCurrentResultCache flashSaleCurrentResultCache,
        IOptions<FlashSalesOptions> options)
        : base(flashSalePlanRepository)
    {
        FlashSalePlanRepository = flashSalePlanRepository;
        ProductAppService = productAppService;
        PreOrderDistributedCache = tokenDistributedCache;
        PlanDistributedCache = planDistributedCache;
        ProductDistributedCache = productDistributedCache;
        DistributedEventBus = distributedEventBus;
        FlashSaleResultRepository = flashSaleResultRepository;
        DistributedLock = distributedLock;
        FlashSalePlanHasher = flashSalePlanHasher;
        FlashSaleInventoryManager = flashSaleInventoryManager;
        FlashSaleCurrentResultCache = flashSaleCurrentResultCache;
        Options = options.Value;
    }

    public override async Task<FlashSalePlanDto> GetAsync(Guid id)
    {
        var flashSalePlan = await GetEntityByIdAsync(id);

        await CheckGetPolicyAsync();

        if (!flashSalePlan.IsPublished)
        {
            await CheckMultiStorePolicyAsync(flashSalePlan.StoreId, FlashSalesPermissions.FlashSalePlan.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalePlan);
    }

    public override async Task<PagedResultDto<FlashSalePlanDto>> GetListAsync(FlashSalePlanGetListInput input)
    {
        await CheckGetListPolicyAsync();

        if (input.IncludeUnpublished)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, FlashSalesPermissions.FlashSalePlan.Manage);
        }

        return await base.GetListAsync(input);
    }

    public override async Task<FlashSalePlanDto> CreateAsync(FlashSalePlanCreateDto input)
    {
        await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

        var product = await ProductAppService.GetAsync(input.ProductId);
        var productSku = product.GetSkuById(input.ProductSkuId);

        await ValidateProductAsync(input.ProductId, product, input.StoreId);

        var flashSalePlan = new FlashSalePlan(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            input.StoreId,
            input.BeginTime,
            input.EndTime,
            product.Id,
            productSku.Id,
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

        await ValidateProductAsync(input.ProductId, product, flashSalePlan.StoreId);

        if (await ExistRelatedFlashSaleResultsAsync(id) && (input.ProductId != flashSalePlan.ProductId || input.ProductSkuId != flashSalePlan.ProductSkuId))
        {
            throw new RelatedFlashSaleResultsExistException(id);
        }

        flashSalePlan.SetTimeRange(input.BeginTime, input.EndTime);
        flashSalePlan.SetProductSku(flashSalePlan.StoreId, product.Id, productSku.Id);
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
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.ProductId.HasValue, x => x.ProductId == input.ProductId.Value)
            .WhereIf(input.ProductSkuId.HasValue, x => x.ProductSkuId == input.ProductSkuId.Value)
            .WhereIf(!input.IncludeUnpublished, x => x.IsPublished)
            .WhereIf(input.Start.HasValue, x => x.BeginTime >= input.Start.Value)
            .WhereIf(input.End.HasValue, x => x.BeginTime <= input.End.Value);
    }

    [Authorize]
    [UnitOfWork(IsDisabled = true)]
    public virtual async Task<FlashSalePlanPreOrderDto> PreOrderAsync(Guid id)
    {
        await CheckPolicyAsync(PreOrderPolicyName);

        var plan = await GetFlashSalePlanCacheAsync(id);
        var product = await GetProductCacheAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);
        var expiresTime = DateTimeOffset.Now.Add(Options.PreOrderExpires);

        await ValidatePreOrderAsync(plan, product, productSku);

        await SetPreOrderCacheAsync(plan, product, productSku, expiresTime);

        return new FlashSalePlanPreOrderDto { ExpiresTime = Clock.Normalize(expiresTime.LocalDateTime), ExpiresInSeconds = Options.PreOrderExpires.TotalSeconds };
    }

    [DisableAuditing]
    [UnitOfWork(IsDisabled = true)]
    [Authorize]
    public virtual async Task<FlashSaleOrderResultDto> OrderAsync(Guid id, OrderFlashSalePlanInput flashSalePlanInput)
    {
        var preOrderCache = await GetPreOrderCacheAsync(id);
        if (preOrderCache == null)
        {
            return CreateFailureResultDto(FlashSalesErrorCodes.PreOrderExpired);
        }

        var plan = await GetFlashSalePlanCacheAsync(id);
        var now = Clock.Now;
        if (plan.BeginTime > now)
        {
            return CreateFailureResultDto(FlashSalesErrorCodes.FlashSaleNotStarted);
        }

        if (now >= plan.EndTime)
        {
            return CreateFailureResultDto(FlashSalesErrorCodes.FlashSaleIsOver);
        }

        await RemovePreOrderCacheAsync(id);

        var userId = CurrentUser.GetId();

        var lockKey = $"create-flash-sale-order-{plan.Id}-{userId}";

        await using var handle = await DistributedLock.TryAcquireAsync(lockKey);

        if (handle == null)
        {
            return CreateFailureResultDto(FlashSalesErrorCodes.BusyToCreateFlashSaleOrder);
        }

        if (await FlashSaleCurrentResultCache.GetAsync(plan.Id, CurrentUser.GetId()) is not null)
        {
            return CreateFailureResultDto(FlashSalesErrorCodes.DuplicateFlashSalesOrder);
        }

        if (!await FlashSaleInventoryManager.TryReduceInventoryAsync(plan.TenantId, preOrderCache.InventoryProviderName,
                plan.StoreId, plan.ProductId, plan.ProductSkuId))
        {
            await FlashSaleCurrentResultCache.RemoveAsync(plan.Id, CurrentUser.GetId());
            return CreateFailureResultDto(FlashSalesErrorCodes.ProductSkuInventoryExceeded);
        }

        try
        {
            var createFlashSaleResultEto =
                await PrepareCreateFlashSaleResultEtoAsync(plan, flashSalePlanInput, userId, Clock.Now, preOrderCache.HashToken);

            await FlashSaleCurrentResultCache.SetAsync(plan.Id, CurrentUser.GetId(), new FlashSaleCurrentResultCacheItem
            {
                TenantId = CurrentTenant.Id,
                ResultDto = new FlashSaleResultDto
                {
                    Id = createFlashSaleResultEto.ResultId,
                    StoreId = createFlashSaleResultEto.Plan.StoreId,
                    PlanId = createFlashSaleResultEto.Plan.Id,
                    UserId = createFlashSaleResultEto.UserId
                }
            });

            await DistributedEventBus.PublishAsync(createFlashSaleResultEto, false, false);

            return new FlashSaleOrderResultDto
            {
                IsSuccess = true
            };
        }
        catch (Exception e)
        {
            Logger.LogWarning("Failed to publish the CreateFlashSaleOrderEto event!");
            Logger.LogException(e, LogLevel.Warning);

            await FlashSaleInventoryManager.TryRollBackInventoryAsync(plan.TenantId,
                preOrderCache.InventoryProviderName, plan.StoreId, plan.ProductId, plan.ProductSkuId);

            await FlashSaleCurrentResultCache.RemoveAsync(plan.Id, CurrentUser.GetId());
            return CreateFailureResultDto(FlashSalesErrorCodes.DistributedEventBusUnavailable);
        }
    }

    protected virtual FlashSaleOrderResultDto CreateFailureResultDto(string errorCode)
    {
        return new FlashSaleOrderResultDto
        {
            IsSuccess = false,
            ErrorCode = errorCode,
            ErrorMessage = L[errorCode]
        };
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

    protected virtual Task<string> GetPreOrderCacheKeyAsync(Guid planId)
    {
        return Task.FromResult(string.Format(PreOrderCacheKeyFormat, planId, CurrentUser.Id));
    }

    protected virtual async Task<FlashSalePlanPreOrderCacheItem> GetPreOrderCacheAsync(Guid planId)
    {
        return await PreOrderDistributedCache.GetAsync(await GetPreOrderCacheKeyAsync(planId));
    }

    protected virtual async Task RemovePreOrderCacheAsync(Guid planId)
    {
        await PreOrderDistributedCache.RemoveAsync(await GetPreOrderCacheKeyAsync(planId));
    }

    protected virtual async Task SetPreOrderCacheAsync(FlashSalePlanCacheItem plan, ProductDto product, ProductSkuDto productSku, DateTimeOffset expirationTime)
    {
        var hashToken = await FlashSalePlanHasher.HashAsync(plan.LastModificationTime, product.LastModificationTime, productSku.LastModificationTime);

        await PreOrderDistributedCache.SetAsync(await GetPreOrderCacheKeyAsync(plan.Id), new FlashSalePlanPreOrderCacheItem()
        {
            HashToken = hashToken,
            PlanId = plan.Id,
            ProductId = product.Id,
            ProductSkuId = productSku.Id,
            InventoryProviderName = product.InventoryProviderName,
        }, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = expirationTime
        });
    }

    #endregion

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

        return Task.CompletedTask;
    }

    protected virtual async Task<bool> ExistRelatedFlashSaleResultsAsync(Guid planId)
    {
        return await FlashSaleResultRepository.AnyAsync(x => x.PlanId == planId);
    }

    protected virtual Task ValidateProductAsync(Guid productId, ProductDto product, Guid storeId)
    {
        if (product.StoreId != storeId)
        {
            throw new ProductIsNotInThisStoreException(productId, storeId);
        }

        if (product.InventoryStrategy != InventoryStrategy.FlashSales)
        {
            throw new UnexpectedInventoryStrategyException(InventoryStrategy.FlashSales);
        }

        return Task.CompletedTask;
    }

    protected virtual Task<CreateFlashSaleResultEto> PrepareCreateFlashSaleResultEtoAsync(
        FlashSalePlanCacheItem plan, OrderFlashSalePlanInput flashSalePlanInput,
        Guid userId, DateTime reducedInventoryTime, string hashToken)
    {
        var planEto = ObjectMapper.Map<FlashSalePlanCacheItem, FlashSalePlanEto>(plan);
        planEto.TenantId = CurrentTenant.Id;

        var eto = new CreateFlashSaleResultEto
        {
            TenantId = CurrentTenant.Id,
            UserId = userId,
            ResultId = GuidGenerator.Create(),
            ReducedInventoryTime = reducedInventoryTime,
            CustomerRemark = flashSalePlanInput.CustomerRemark,
            Plan = planEto,
            HashToken = hashToken
        };

        if (flashSalePlanInput.ExtraProperties != null)
        {
            flashSalePlanInput.MapExtraPropertiesTo(eto, MappingPropertyDefinitionChecks.Source);
        }

        return Task.FromResult(eto);
    }

    protected virtual async Task<ProductCacheItem> GetProductCacheAsync(Guid productId)
    {
        return await ProductDistributedCache.GetOrAddAsync(productId, async () =>
        {
            var productDto = await ProductAppService.GetAsync(productId);

            var cacheItem = ObjectMapper.Map<ProductDto, ProductCacheItem>(productDto);

            if (cacheItem != null)
            {
                cacheItem.TenantId = CurrentTenant.Id;
            }

            return cacheItem;
        });
    }
}
