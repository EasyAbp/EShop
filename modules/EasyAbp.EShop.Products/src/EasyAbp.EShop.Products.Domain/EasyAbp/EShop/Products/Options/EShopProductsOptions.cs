using EasyAbp.EShop.Products.Options.InventoryProviders;
using EasyAbp.EShop.Products.Options.ProductGroups;
using EasyAbp.EShop.Products.Products;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Options
{
    public class EShopProductsOptions
    {
        public ProductGroupConfigurations Groups { get; } = new();

        public InventoryProviderConfigurations InventoryProviders { get; } = new();

        /// <summary>
        /// If the value is <c>null</c>, it will fall back to DefaultProductInventoryProviderName
        /// in the <see cref="DefaultProductInventoryProvider"/>.
        /// </summary>
        [CanBeNull]
        public string DefaultInventoryProviderName { get; set; }
    }
}