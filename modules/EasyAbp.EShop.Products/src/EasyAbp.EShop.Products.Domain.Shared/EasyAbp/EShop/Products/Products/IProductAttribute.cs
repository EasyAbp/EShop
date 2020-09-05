using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttribute : IHasExtraProperties
    {
        string DisplayName { get; }
        
        string Description { get; }
        
        int DisplayOrder { get; }
    }
}