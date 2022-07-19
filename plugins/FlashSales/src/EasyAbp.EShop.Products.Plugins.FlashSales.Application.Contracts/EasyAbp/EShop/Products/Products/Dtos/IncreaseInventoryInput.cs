using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products.Dtos;

public class IncreaseInventoryInput : IMultiTenant, IMultiStore
{
    public Guid? TenantId { get; set; }

    public string ProviderName { get; set; }

    public Guid StoreId { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public int Quantity { get; set; }

    public bool ReduceSold { get; set; }

    public IncreaseInventoryInput()
    {
    }

    public IncreaseInventoryInput(Guid? tenantId, string providerName, Guid storeId, Guid productId, Guid productSkuId, int quantity, bool reduceSold)
    {
        TenantId = tenantId;
        ProviderName = providerName;
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
        Quantity = quantity;
        ReduceSold = reduceSold;
    }
}
