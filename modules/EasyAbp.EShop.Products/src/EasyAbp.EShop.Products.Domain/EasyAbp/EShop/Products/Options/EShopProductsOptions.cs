using System;
using EasyAbp.EShop.Products.Options.ProductGroups;

namespace EasyAbp.EShop.Products.Options
{
    public class EShopProductsOptions
    {
        public ProductGroupConfigurations Groups { get; }

        public Type DefaultFileDownloadProviderType { get; set; }
        
        public EShopProductsOptions()
        {
            Groups = new ProductGroupConfigurations();
        }
    }
}