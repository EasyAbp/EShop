using System;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProduct : IHasExtraProperties
    {
        Guid ProductTypeId { get; }
        
        Guid ProductDetailId { get; }

        string Code { get; }

        string DisplayName { get; }
        
        InventoryStrategy InventoryStrategy { get; }
        
        string MediaResources { get; }
        
        int DisplayOrder { get; }

        bool IsPublished { get; }
        
        bool IsStatic { get; }
        
        bool IsHidden { get; }
    }
}