using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProduct : IHasExtraProperties, IMultiStore
    {
        string ProductGroupName { get; }
        
        Guid? ProductDetailId { get; }

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