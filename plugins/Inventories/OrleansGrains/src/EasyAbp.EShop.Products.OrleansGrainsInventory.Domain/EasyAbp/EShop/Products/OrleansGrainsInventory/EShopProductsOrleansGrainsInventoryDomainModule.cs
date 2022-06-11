using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;
using EasyAbp.EShop.Products.Options;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory;

[DependsOn(
    typeof(EShopProductsDomainModule),
    typeof(EShopPluginsInventoriesOrleansGrainsAbstractionsModule)
)]
public class EShopProductsOrleansGrainsInventoryDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<EShopProductsOptions>(options =>
        {
            options.InventoryProviders.Configure(
                OrleansGrainsProductInventoryProvider.OrleansGrainsProductInventoryProviderName, provider =>
                {
                    provider.DisplayName =
                        OrleansGrainsProductInventoryProvider.OrleansGrainsProductInventoryProviderDisplayName;
                    provider.Description = OrleansGrainsProductInventoryProvider
                        .OrleansGrainsProductInventoryProviderDescription;
                    provider.ProviderType = typeof(OrleansGrainsProductInventoryProvider);
                });
        });
    }
}