namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttributeOption
    {
        string DisplayName { get; }
        
        string Description { get; }
        
        int DisplayOrder { get; }
    }
}