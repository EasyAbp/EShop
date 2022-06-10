using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductInventories;

public class InventoryQueryModel : IMultiTenant, IMultiStore
{
    public Guid? TenantId { get; set; }

    public Guid StoreId { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public InventoryQueryModel()
    {
    }

    public InventoryQueryModel(Guid? tenantId, Guid storeId, Guid productId, Guid productSkuId)
    {
        TenantId = tenantId;
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
    }
}