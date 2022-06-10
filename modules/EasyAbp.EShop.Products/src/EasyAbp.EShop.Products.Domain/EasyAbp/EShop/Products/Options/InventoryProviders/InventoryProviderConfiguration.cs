using System;

namespace EasyAbp.EShop.Products.Options.InventoryProviders
{
    public class InventoryProviderConfiguration
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Type ProviderType { get; set; }
    }
}