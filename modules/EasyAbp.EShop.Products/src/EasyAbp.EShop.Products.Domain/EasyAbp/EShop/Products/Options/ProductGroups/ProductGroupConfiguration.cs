using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Options.ProductGroups
{
    public class ProductGroupConfiguration
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// If the value is <c>null</c>, it will fall back to DefaultInventoryProviderName
        /// in the <see cref="EShopProductsOptions"/>.
        /// </summary>
        [CanBeNull]
        public string DefaultInventoryProviderName { get; set; }
    }
}