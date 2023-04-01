using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Products.Products;
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

    public virtual async Task<Money?> GetUnitPriceOrNullAsync(ICreateOrderInfo input,
        ICreateOrderLineInfo inputOrderLine, IProduct product, IProductSku productSku, Currency effectiveCurrency)
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

    public virtual async Task<Money?> GetAssetBookingUnitPriceAsync(ICreateOrderInfo input,
        ICreateOrderLineInfo inputOrderLine, Currency effectiveCurrency)
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
            await CheckCurrencyAsync(productAssetPeriod.Currency, effectiveCurrency);

            return new Money(productAssetPeriod.Price, effectiveCurrency);
        }

        if (productAsset.Price.HasValue)
        {
            await CheckCurrencyAsync(productAsset.Currency, effectiveCurrency);

            return new Money(productAsset.Price.Value, effectiveCurrency);
        }

        return null;
    }

    public virtual async Task<Money?> GetAssetCategoryBookingUnitPriceAsync(ICreateOrderInfo input,
        ICreateOrderLineInfo inputOrderLine, Currency effectiveCurrency)
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
            await CheckCurrencyAsync(productAssetCategoryPeriod.Currency, effectiveCurrency);

            return new Money(productAssetCategoryPeriod.Price, effectiveCurrency);
        }

        if (productAssetCategory.Price.HasValue)
        {
            await CheckCurrencyAsync(productAssetCategory.Currency, effectiveCurrency);

            return new Money(productAssetCategory.Price.Value, effectiveCurrency);
        }

        return null;
    }

    protected virtual Task CheckCurrencyAsync(string currency, Currency effectiveCurrency)
    {
        if (currency != effectiveCurrency.Code)
        {
            throw new UnexpectedCurrencyException(effectiveCurrency.Code);
        }

        return Task.CompletedTask;
    }
}