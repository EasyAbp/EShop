using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Options.InventoryProviders
{
    public class InventoryProviderConfigurationProvider : IInventoryProviderConfigurationProvider, ITransientDependency
    {
        private readonly EShopProductsOptions _options;

        public InventoryProviderConfigurationProvider(IOptions<EShopProductsOptions> options)
        {
            _options = options.Value;
        }

        public InventoryProviderConfiguration Get(string providerName)
        {
            return _options.InventoryProviders.GetConfiguration(providerName);
        }
    }
}