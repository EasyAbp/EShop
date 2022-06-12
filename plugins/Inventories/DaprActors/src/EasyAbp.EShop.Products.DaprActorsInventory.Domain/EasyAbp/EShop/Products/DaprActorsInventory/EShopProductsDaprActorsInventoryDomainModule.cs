using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using EasyAbp.EShop.Products.Options;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.DaprActorsInventory;

[DependsOn(
    typeof(EShopProductsDomainModule),
    typeof(EShopPluginsInventoriesDaprActorsAbstractionsModule)
)]
public class EShopProductsDaprActorsInventoryDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<EShopProductsOptions>(options =>
        {
            options.InventoryProviders.Configure(
                DaprActorsProductInventoryProvider.DaprActorsProductInventoryProviderName, provider =>
                {
                    provider.DisplayName =
                        DaprActorsProductInventoryProvider.DaprActorsProductInventoryProviderDisplayName;
                    provider.Description = DaprActorsProductInventoryProvider
                        .DaprActorsProductInventoryProviderDescription;
                    provider.ProviderType = typeof(DaprActorsProductInventoryProvider);
                });
        });
    }
}