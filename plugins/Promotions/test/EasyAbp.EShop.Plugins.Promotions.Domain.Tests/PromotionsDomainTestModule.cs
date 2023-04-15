using EasyAbp.EShop.Orders.Plugins.Promotions;
using EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;
using EasyAbp.EShop.Products.Plugins.Promotions;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(PromotionsEntityFrameworkCoreTestModule),
    typeof(EShopProductsPluginsPromotionsDomainModule),
    typeof(EShopOrdersPluginsPromotionsDomainModule)
)]
public class PromotionsDomainTestModule : AbpModule
{

}
