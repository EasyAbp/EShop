using System;
using System.Collections.Generic;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProduct : IHasExtraProperties
    {
        string ProductGroupName { get; }
        
        Guid ProductDetailId { get; }

        string UniqueName { get; }

        string DisplayName { get; }
        
        InventoryStrategy InventoryStrategy { get; }
        
        string MediaResources { get; }
        
        int DisplayOrder { get; }

        bool IsPublished { get; }
        
        bool IsStatic { get; }
        
        bool IsHidden { get; }
    }
}