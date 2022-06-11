using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory.Domain;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAuthorizationModule),
    typeof(EShopProductsOrleansGrainsInventoryDomainModule)
)]
public class EShopProductsOrleansGrainsInventoryDomainTestModule : AbpModule
{
}