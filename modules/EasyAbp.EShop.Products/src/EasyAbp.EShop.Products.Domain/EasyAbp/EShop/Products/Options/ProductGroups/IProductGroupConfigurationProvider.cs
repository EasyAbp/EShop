namespace EasyAbp.EShop.Products.Options.ProductGroups
{
    public interface IProductGroupConfigurationProvider
    {
        ProductGroupConfiguration Get(string productGroupName);
    }
}