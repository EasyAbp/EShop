using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using NodaMoney;

namespace EasyAbp.EShop.Orders.Orders;

public interface IOrderLinePriceOverrider
{
    Task<Money?> GetUnitPriceOrNullAsync(CreateOrderDto input, CreateOrderLineDto inputOrderLine, ProductDto product,
        ProductSkuDto productSku, Currency effectiveCurrency);
}