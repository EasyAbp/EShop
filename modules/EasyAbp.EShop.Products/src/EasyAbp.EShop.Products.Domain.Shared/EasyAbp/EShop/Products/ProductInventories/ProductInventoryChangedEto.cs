using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductInventories
{
    [Serializable]
    public class ProductInventoryChangedEto : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid StoreId { get; set; }
        
        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public int OriginalInventory { get; set; }
        
        public int NewInventory { get; set; }
        
        public long Sold { get; set; }

        public ProductInventoryChangedEto(
            Guid? tenantId,
            Guid storeId,
            Guid productId,
            Guid productSkuId,
            int originalInventory,
            int newInventory,
            long sold)
        {
            TenantId = tenantId;
            StoreId = storeId;
            ProductId = productId;
            ProductSkuId = productSkuId;
            OriginalInventory = originalInventory;
            NewInventory = newInventory;
            Sold = sold;
        }
    }
}