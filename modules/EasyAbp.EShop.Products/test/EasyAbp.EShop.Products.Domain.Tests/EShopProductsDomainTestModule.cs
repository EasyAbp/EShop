using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopProductsEntityFrameworkCoreTestModule)
        )]
    public class EShopProductsDomainTestModule : AbpModule
    {
        
    }
}
