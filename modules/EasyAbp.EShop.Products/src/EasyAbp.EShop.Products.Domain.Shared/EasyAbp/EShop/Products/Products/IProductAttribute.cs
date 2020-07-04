using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttribute
    {
        string DisplayName { get; }
        
        string Description { get; }
        
        int DisplayOrder { get; }
    }
}