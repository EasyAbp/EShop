using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IFlashSaleInventoryManager
{
    Task<bool> TryReduceInventoryAsync(Guid? tenantId, string providerName, Guid storeId, Guid productId,
        Guid productSkuId);

    Task<bool> TryRollBackInventoryAsync(Guid? tenantId, string providerName, Guid storeId, Guid productId,
        Guid productSkuId);
}