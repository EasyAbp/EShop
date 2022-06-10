using EasyAbp.EShop.Products;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

[DependsOn(
    typeof(EShopProductsDomainSharedModule)
)]
public class EShopPluginsInventoriesDaprActorsAbstractionsModule : AbpModule
{
}