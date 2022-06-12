using EasyAbp.EShop.Products;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[DependsOn(
    typeof(EShopProductsDomainSharedModule)
)]
public class EShopPluginsInventoriesOrleansGrainsAbstractionsModule : AbpModule
{
}