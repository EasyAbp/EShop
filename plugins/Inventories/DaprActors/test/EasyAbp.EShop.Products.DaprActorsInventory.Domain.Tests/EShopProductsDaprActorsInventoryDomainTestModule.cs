using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.DaprActorsInventory.Domain;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpTestBaseModule),
    typeof(AbpAuthorizationModule),
    typeof(EShopProductsDaprActorsInventoryDomainModule)
)]
public class EShopProductsDaprActorsInventoryDomainTestModule : AbpModule
{
}