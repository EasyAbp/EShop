using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
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
    
    public virtual async Task<decimal?> GetUnitPriceOrNullAsync(CreateOrderDto input, CreateOrderLineDto inputOrderLine,
        ProductDto product, ProductSkuDto productSku)
    {
        if (inputOrderLine.FindBookingAssetId() is not null)
        {
            return await GetAssetBookingUnitPriceAsync(input, inputOrderLine);
        }

        if (inputOrderLine.FindBookingAssetCategoryId() is not null)
        {
            return await GetAssetCategoryBookingUnitPriceAsync(input, inputOrderLine);
        }

        return null;
    }

    public virtual async Task<decimal?> GetAssetBookingUnitPriceAsync(CreateOrderDto input,
        CreateOrderLineDto inputOrderLine)
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
            return productAssetPeriod.Price;
        }

        return productAsset.Price;
    }
    
    public virtual async Task<decimal?> GetAssetCategoryBookingUnitPriceAsync(CreateOrderDto input,
        CreateOrderLineDto inputOrderLine)
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
            return productAssetCategoryPeriod.Price;
        }

        return productAssetCategory.Price;
    }
}