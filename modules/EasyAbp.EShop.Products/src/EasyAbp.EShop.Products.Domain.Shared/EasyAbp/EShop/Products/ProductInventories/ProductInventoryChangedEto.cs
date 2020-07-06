using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryChangedEto
    {
        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public int OriginalInventory { get; set; }
        
        public int NewInventory { get; set; }
        
        public long Sold { get; set; }
    }
}