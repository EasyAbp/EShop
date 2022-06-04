using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class TestOrderLinePriceOverrider : IOrderLinePriceOverrider, ITransientDependency
{
    public static decimal Sku2UnitPrice { get; set; } = 100m;
    
    public async Task<decimal?> GetUnitPriceOrNullAsync(CreateOrderDto input, CreateOrderLineDto inputOrderLine,
        ProductDto product, ProductSkuDto productSku)
    {
        if (inputOrderLine.ProductSkuId == OrderTestData.ProductSku2Id)
        {
            return Sku2UnitPrice;
        }

        return null;
    }
}