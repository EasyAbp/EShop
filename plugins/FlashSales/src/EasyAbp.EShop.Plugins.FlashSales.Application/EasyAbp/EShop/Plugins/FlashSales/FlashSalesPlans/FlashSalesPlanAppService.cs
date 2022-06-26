using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Data;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Users;
using static EasyAbp.EShop.Products.Permissions.ProductsPermissions;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Authorize]
public class FlashSalesPlanAppService :
    CrudAppService<FlashSalesPlan, FlashSalesPlanDto, Guid, FlashSalesPlanGetListInput, FlashSalesPlanCreateDto, FlashSalesPlanUpdateDto>,
    IFlashSalesPlanAppService
{
    protected override string GetPolicyName { get; set; }
    protected override string GetListPolicyName { get; set; }
    protected override string CreatePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Create;
    protected override string UpdatePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Update;
    protected override string DeletePolicyName { get; set; } = FlashSalesPermissions.FlashSalesPlan.Delete;

    protected IFlashSalesPlanRepository FlashSalesPlanRepository { get; }
    protected IProductAppService ProductAppService { get; }
    protected IProductDetailAppService ProductDetailAppService { get; }
    protected IDistributedCache TokenDistributedCache { get; }
    protected IDistributedCache<FlashSalesPlanCacheItem, Guid> DistributedCache { get; }
    protected IDistributedEventBus DistributedEventBus { get; }
    protected FlashSalesPlanManager FlashSalesPlanManager { get; }
    protected IFlashSalesResultRepository FlashSalesResultRepository { get; }
    protected IAbpDistributedLock DistributedLock { get; }

    public FlashSalesPlanAppService(
        IFlashSalesPlanRepository flashSalesPlanRepository,
        IProductAppService productAppService,
        IProductDetailAppService productDetailAppService,
        IDistributedCache tokenDistributedCache,
        IDistributedCache<FlashSalesPlanCacheItem, Guid> distributedCache,
        IDistributedEventBus distributedEventBus,
        FlashSalesPlanManager flashSalesPlanManager,
        IFlashSalesResultRepository flashSalesResultRepository,
        IAbpDistributedLock distributedLock)
        : base(flashSalesPlanRepository)
    {
        FlashSalesPlanRepository = flashSalesPlanRepository;
        ProductAppService = productAppService;
        ProductDetailAppService = productDetailAppService;
        TokenDistributedCache = tokenDistributedCache;
        DistributedCache = distributedCache;
        DistributedEventBus = distributedEventBus;
        FlashSalesPlanManager = flashSalesPlanManager;
        FlashSalesResultRepository = flashSalesResultRepository;
        DistributedLock = distributedLock;
    }

    public override async Task<FlashSalesPlanDto> GetAsync(Guid id)
    {
        await CheckGetPolicyAsync();

        var flashSalesPlan = await GetEntityByIdAsync(id);

        if (!flashSalesPlan.IsPublished)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSalesPlan.Manage);
        }

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    public override async Task<FlashSalesPlanDto> CreateAsync(FlashSalesPlanCreateDto input)
    {
        await CheckCreatePolicyAsync();

        var flashSalesPlan = await FlashSalesPlanManager.CreateAsync(
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
        await CheckUpdatePolicyAsync();

        var flashSalesPlan = await GetEntityByIdAsync(id);

        await FlashSalesPlanManager.UpdateAsync(
            flashSalesPlan,
            input.BeginTime,
            input.EndTime,
            input.ProductId,
            input.ProductSkuId,
            input.IsPublished
        );

        flashSalesPlan.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        await FlashSalesPlanRepository.UpdateAsync(flashSalesPlan, autoSave: true);

        return await MapToGetOutputDtoAsync(flashSalesPlan);
    }

    protected override async Task<IQueryable<FlashSalesPlan>> CreateFilteredQueryAsync(FlashSalesPlanGetListInput input)
    {
        if (!input.OnlyShowPublished)
        {
            await CheckPolicyAsync(FlashSalesPermissions.FlashSalesPlan.Manage);
        }

        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId.Value)
            .WhereIf(input.ProductId.HasValue, x => x.ProductId == input.ProductId.Value)
            .WhereIf(input.ProductSkuId.HasValue, x => x.ProductSkuId == input.ProductSkuId.Value)
            .WhereIf(input.OnlyShowPublished, x => x.IsPublished)
            .WhereIf(input.Start.HasValue, x => x.BeginTime >= input.Start.Value)
            .WhereIf(input.End.HasValue, x => x.BeginTime <= input.End.Value);
    }

    public virtual async Task PreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);
        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        if (!product.IsPublished)
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductIsNotPublished);
        }

        if (product.InventoryStrategy != InventoryStrategy.FlashSales)
        {
            throw new BusinessException(FlashSalesErrorCodes.IsNotFlashSalesProduct);
        }

        if (!plan.IsPublished)
        {
            throw new EntityNotFoundException(typeof(FlashSalesPlan), id);
        }

        if (Clock.Now > plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSalesPlanIsExpired);
        }

        await SetCacheHashTokenAsync(plan, product, productSku);
    }

    public virtual async Task CheckPreOrderAsync(Guid id)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);
        if (Clock.Now > plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSalesPlanIsExpired);
        }

        var cacheHashToken = await GetCacheHashTokenAsync(plan);
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExipred);
        }

        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        if (!await ComparekHashTokenAsync(cacheHashToken, plan, product, productSku))
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExipred);
        }
    }

    public virtual async Task CreateOrderAsync(Guid id, CreateOrderInput input)
    {
        var plan = await GetFlashSalesPlanCacheAsync(id);
        var now = Clock.Now;
        if (plan.BeginTime > now)
        {
            throw new BusinessException(FlashSalesErrorCodes.ProductIsNotPublished);
        }

        if (now > plan.EndTime)
        {
            throw new BusinessException(FlashSalesErrorCodes.FlashSalesPlanIsExpired);
        }

        var cacheHashToken = await GetCacheHashTokenAsync(plan);
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExipred);
        }

        var product = await ProductAppService.GetAsync(plan.ProductId);
        var productSku = product.GetSkuById(plan.ProductSkuId);

        if (!await ComparekHashTokenAsync(cacheHashToken, plan, product, productSku))
        {
            throw new BusinessException(FlashSalesErrorCodes.PreOrderExipred);
        }

        await RemoveCacheHashTokenAsync(plan);

        var userId = CurrentUser.GetId();

        var result = await CreatePendingFlashSalesResultAsync(plan, userId);

        var CreateFlashSalesOrderEto = await PrepareCreateFlashSalesOrderEtoAsync(plan, product, productSku, result, input, userId, now);

        await DistributedEventBus.PublishAsync(CreateFlashSalesOrderEto);
    }

    protected virtual async Task<CreateFlashSalesOrderEto> PrepareCreateFlashSalesOrderEtoAsync(
        FlashSalesPlanDto plan,
        ProductDto product,
        ProductSkuDto productSku,
        FlashSalesResult result,
        CreateOrderInput input,
        Guid userId,
        DateTime now)
    {
        FlashSalesProductDetailEto productDetail = null;
        var productDetailId = productSku.ProductDetailId ?? product.ProductDetailId;
        if (productDetailId.HasValue)
        {
            productDetail = ObjectMapper.Map<ProductDetailDto, FlashSalesProductDetailEto>(await ProductDetailAppService.GetAsync(productDetailId.Value));
        }
        var productEto = ObjectMapper.Map<ProductDto, FlashSalesProductEto>(product);
        productEto.TenantId = CurrentTenant.Id;
        var planEto = ObjectMapper.Map<FlashSalesPlanDto, FlashSalesPlanEto>(plan);
        planEto.TenantId = CurrentTenant.Id;

        var eto = new CreateFlashSalesOrderEto()
        {
            TenantId = CurrentTenant.Id,
            PlanId = plan.Id,
            UserId = userId,
            PendingResultId = result.Id,
            StoreId = plan.StoreId,
            CreateTime = now,
            CustomerRemark = input.CustomerRemark,
            Quantity = 1,//should configure
            Product = productEto,
            ProductDetail = productDetail,
            Plan = planEto
        };

        foreach (var item in input.ExtraProperties)
        {
            eto.ExtraProperties.Add(item.Key, item.Value);
        }

        return eto;
    }

    protected virtual async Task<FlashSalesPlanCacheItem> GetFlashSalesPlanCacheAsync(Guid id)
    {
        return await DistributedCache.GetOrAddAsync(id, async () =>
        {
            var flashSalesPlan = await FlashSalesPlanRepository.GetAsync(id);
            return ObjectMapper.Map<FlashSalesPlan, FlashSalesPlanCacheItem>(flashSalesPlan);
        });
    }

    protected virtual Task<string> GetCacheKeyAsync(FlashSalesPlanDto plan)
    {
        return Task.FromResult($"eshopflashsales_{CurrentUser.Id}_{plan.ProductSkuId}");
    }

    protected virtual async Task<string> GetCacheHashTokenAsync(FlashSalesPlanDto plan)
    {
        return await TokenDistributedCache.GetStringAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task RemoveCacheHashTokenAsync(FlashSalesPlanDto plan)
    {
        await TokenDistributedCache.RemoveAsync(await GetCacheKeyAsync(plan));
    }

    protected virtual async Task SetCacheHashTokenAsync(FlashSalesPlanDto plan, ProductDto product, ProductSkuDto productSku)
    {
        await TokenDistributedCache.SetStringAsync(await GetCacheKeyAsync(plan), await GetHashTokenAsync(plan, product, productSku), new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
        });
    }

    protected virtual Task<string> GetHashTokenAsync(FlashSalesPlanDto plan, ProductDto product, ProductSkuDto productSku)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes($"{plan.LastModificationTime}|{product.LastModificationTime}|{productSku.LastModificationTime}");
        var sb = new StringBuilder();
        foreach (var t in md5.ComputeHash(inputBytes))
        {
            sb.Append(t.ToString("X2"));
        }
        return Task.FromResult(sb.ToString());
    }

    protected virtual async Task<bool> ComparekHashTokenAsync(string cacheHashToken, FlashSalesPlanDto plan, ProductDto product, ProductSkuDto productSku)
    {
        if (cacheHashToken.IsNullOrWhiteSpace())
        {
            return false;
        }

        var hashToken = await GetHashTokenAsync(plan, product, productSku);

        return cacheHashToken == hashToken;
    }

    protected virtual async Task<FlashSalesResult> CreatePendingFlashSalesResultAsync(FlashSalesPlanDto plan, Guid userId)
    {
        var lockKey = $"create-flash-sales-order-{plan.Id}-{userId}";

        await using var handle = await DistributedLock.TryAcquireAsync(lockKey);

        if (handle == null)
        {
            throw new BusinessException(FlashSalesErrorCodes.CreateFlashSalesOrderBusy);
        }

        // Prevent repeat submit
        if (await FlashSalesResultRepository.AnyAsync(x => x.PlanId == plan.Id && x.UserId == userId))
        {
            throw new BusinessException(FlashSalesErrorCodes.AlreadySubmitCreateFlashSalesOrder);
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
}
