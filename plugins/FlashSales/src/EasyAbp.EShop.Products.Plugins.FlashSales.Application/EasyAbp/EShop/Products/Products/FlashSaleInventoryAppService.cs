using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Products.Products;

public class FlashSaleInventoryAppService : ProductsAppService, IFlashSaleInventoryAppService
{
    protected ILocalFlashSaleInventoryManager LocalFlashSaleInventoryManager { get; }

    public FlashSaleInventoryAppService(ILocalFlashSaleInventoryManager localFlashSaleInventoryManager)
    {
        LocalFlashSaleInventoryManager = localFlashSaleInventoryManager;
    }

    public virtual async Task<bool> TryReduceAsync(ReduceInventoryInput input)
    {
        await CheckPolicyAsync(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Reduce);

        return await LocalFlashSaleInventoryManager.TryReduceInventoryAsync(
            input.TenantId,
            input.ProviderName,
            input.StoreId,
            input.ProductId,
            input.ProductSkuId,
            input.Quantity,
            input.IncreaseSold
        );
    }

    public virtual async Task<bool> TryIncreaseAsync(IncreaseInventoryInput input)
    {
        await CheckPolicyAsync(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Increase);

        return await LocalFlashSaleInventoryManager.TryRollBackInventoryAsync(
            input.TenantId,
            input.ProviderName,
            input.StoreId,
            input.ProductId,
            input.ProductSkuId,
            input.Quantity,
            input.ReduceSold
        );
    }
}
