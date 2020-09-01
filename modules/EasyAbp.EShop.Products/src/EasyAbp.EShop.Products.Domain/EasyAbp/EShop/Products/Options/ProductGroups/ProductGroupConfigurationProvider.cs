using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Options.ProductGroups
{
    public class ProductGroupConfigurationProvider : IProductGroupConfigurationProvider, ITransientDependency
    {
        private readonly EShopProductsOptions _options;

        public ProductGroupConfigurationProvider(IOptions<EShopProductsOptions> options)
        {
            _options = options.Value;
        }
        
        public ProductGroupConfiguration Get(string productGroupName)
        {
            return _options.Groups.GetConfiguration(productGroupName);
        }
    }
}