using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public interface IPromotionHandler
{
    Task HandleProductAsync(ProductDiscountContext context, Promotion promotion);

    Task HandleOrderAsync(OrderDiscountContext context, Promotion promotion);

    Task<object?> CreateConfigurationsObjectOrNullAsync();

    bool IsConfigurationValid(string configurations);
}