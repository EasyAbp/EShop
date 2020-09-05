using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttributeOption : IHasExtraProperties
    {
        string DisplayName { get; }
        
        string Description { get; }
        
        int DisplayOrder { get; }
    }
}