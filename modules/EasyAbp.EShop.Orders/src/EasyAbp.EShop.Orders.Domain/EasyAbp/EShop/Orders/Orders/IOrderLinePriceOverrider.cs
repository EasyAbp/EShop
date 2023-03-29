using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using NodaMoney;

namespace EasyAbp.EShop.Orders.Orders;

public interface IOrderLinePriceOverrider
{
    Task<Money?> GetUnitPriceOrNullAsync(ICreateOrderInfo input, ICreateOrderLineInfo inputOrderLine,
        IProduct product, IProductSku productSku, Currency effectiveCurrency);
}