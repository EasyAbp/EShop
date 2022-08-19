using System;
using System.Threading.Tasks;

namespace EasyAbp.Eshop.Products.Products;

public interface IFlashSaleInventoryManager
{
    Task<bool> TryReduceInventoryAsync(Guid? tenantId, string providerName, Guid storeId, Guid productId,
        Guid productSkuId);

    Task<bool> TryRollBackInventoryAsync(Guid? tenantId, string providerName, Guid storeId, Guid productId,
        Guid productSkuId);
}