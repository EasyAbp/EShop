using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductInventories
{
    [Serializable]
    public class ProductInventoryChangedEto
    {
        public Guid StoreId { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public int OriginalInventory { get; set; }
        
        public int NewInventory { get; set; }
        
        public long Sold { get; set; }
    }
}