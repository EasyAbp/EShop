using EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Promotions;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(PromotionsEntityFrameworkCoreTestModule)
    )]
public class PromotionsDomainTestModule : AbpModule
{

}
