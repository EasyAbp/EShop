using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using NodaMoney;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class TestOrderLinePriceOverrider : IOrderLinePriceOverrider, ITransientDependency
{
    public static decimal Sku3UnitPrice { get; set; } = 100m;

    public async Task<Money?> GetUnitPriceOrNullAsync(ICreateOrderInfo input, ICreateOrderLineInfo inputOrderLine,
        IProduct product, IProductSku productSku, Currency effectiveCurrency)
    {
        if (inputOrderLine.ProductSkuId == OrderTestData.ProductSku3Id)
        {
            return new Money(Sku3UnitPrice, effectiveCurrency);
        }

        return null;
    }
}