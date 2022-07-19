using System;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;

namespace EasyAbp.EShop.Products.Products;

public class FakeFlashSaleInventoryManager : IFlashSaleInventoryManager
{
    public static bool ShouldReduceSuccess { get; set; }

    public FakeFlashSaleInventoryManager()
    {
        ShouldReduceSuccess = true;
    }

    public Task<bool> TryReduceInventoryAsync(
        Guid? tenantId, string providerName, Guid storeId, Guid productId, Guid productSkuId, int quantity, bool increaseSold)
    {
        return Task.FromResult(ShouldReduceSuccess);
    }

    public Task<bool> TryRollBackInventoryAsync(
        Guid? tenantId, string providerName, Guid storeId, Guid productId, Guid productSkuId, int quantity, bool decreaseSold)
    {
        return Task.FromResult(true);
    }
}
