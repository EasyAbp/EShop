using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using NodaMoney;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Booking;

public class BookingOrderLinePriceOverrider : IOrderLinePriceOverrider, ITransientDependency
{
    private readonly IProductAssetAppService _productAssetAppService;
    private readonly IProductAssetCategoryAppService _productAssetCategoryAppService;

    public BookingOrderLinePriceOverrider(
        IProductAssetAppService productAssetAppService,
        IProductAssetCategoryAppService productAssetCategoryAppService)
    {
        _productAssetAppService = productAssetAppService;
        _productAssetCategoryAppService = productAssetCategoryAppService;
    }

    public virtual async Task<Money?> GetUnitPriceOrNullAsync(CreateOrderDto input, CreateOrderLineDto inputOrderLine,
        ProductDto product, ProductSkuDto productSku, Currency effectiveCurrency)
    {
        if (inputOrderLine.FindBookingAssetId() is not null)
        {
            return await GetAssetBookingUnitPriceAsync(input, inputOrderLine, effectiveCurrency);
        }

        if (inputOrderLine.FindBookingAssetCategoryId() is not null)
        {
            return await GetAssetCategoryBookingUnitPriceAsync(input, inputOrderLine, effectiveCurrency);
        }

        return null;
    }

    public virtual async Task<Money?> GetAssetBookingUnitPriceAsync(CreateOrderDto input,
        CreateOrderLineDto inputOrderLine, Currency effectiveCurrency)
    {
        var productAsset = (await _productAssetAppService.GetListAsync(
            new GetProductAssetListDto
            {
                MaxResultCount = 1,
                StoreId = input.StoreId,
                ProductId = inputOrderLine.ProductId,
                ProductSkuId = inputOrderLine.ProductSkuId,
                AssetId = inputOrderLine.GetBookingAssetId(),
                PeriodSchemeId = inputOrderLine.GetBookingPeriodSchemeId()
            }
        )).Items.First();

        var productAssetPeriod =
            productAsset.Periods.FirstOrDefault(x => x.PeriodId == inputOrderLine.GetBookingPeriodId());

        if (productAssetPeriod is not null)
        {
            return new Money(productAssetPeriod.Price, effectiveCurrency);
        }

        return productAsset.Price.HasValue ? new Money(productAsset.Price.Value, effectiveCurrency) : null;
    }

    public virtual async Task<Money?> GetAssetCategoryBookingUnitPriceAsync(CreateOrderDto input,
        CreateOrderLineDto inputOrderLine, Currency effectiveCurrency)
    {
        var productAssetCategory = (await _productAssetCategoryAppService.GetListAsync(
            new GetProductAssetCategoryListDto
            {
                MaxResultCount = 1,
                StoreId = input.StoreId,
                ProductId = inputOrderLine.ProductId,
                ProductSkuId = inputOrderLine.ProductSkuId,
                AssetCategoryId = inputOrderLine.GetBookingAssetCategoryId(),
                PeriodSchemeId = inputOrderLine.GetBookingPeriodSchemeId()
            }
        )).Items.First();

        var productAssetCategoryPeriod =
            productAssetCategory.Periods.FirstOrDefault(x => x.PeriodId == inputOrderLine.GetBookingPeriodId());

        if (productAssetCategoryPeriod is not null)
        {
            return new Money(productAssetCategoryPeriod.Price, effectiveCurrency);
        }

        return productAssetCategory.Price.HasValue
            ? new Money(productAssetCategory.Price.Value, effectiveCurrency)
            : null;
    }
}