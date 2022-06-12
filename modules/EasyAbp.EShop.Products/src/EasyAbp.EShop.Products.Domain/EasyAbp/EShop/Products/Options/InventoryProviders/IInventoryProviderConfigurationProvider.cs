namespace EasyAbp.EShop.Products.Options.InventoryProviders
{
    public interface IInventoryProviderConfigurationProvider
    {
        InventoryProviderConfiguration Get(string providerName);
    }
}