using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Products.Products;

public class FlashSaleInventoryAppService : ProductsAppService, IFlashSaleInventoryAppService
{
    protected ILocalFlashSaleInventoryManager LocalFlashSaleInventoryReducer { get; }

    public FlashSaleInventoryAppService(ILocalFlashSaleInventoryManager localFlashSaleInventoryReducer)
    {
        LocalFlashSaleInventoryReducer = localFlashSaleInventoryReducer;
    }

    public virtual async Task<bool> TryReduceInventoryAsync(ReduceInventoryInput input)
    {
        await CheckPolicyAsync(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Reduce);

        return await LocalFlashSaleInventoryReducer.TryReduceInventoryAsync(
            input.TenantId,
            input.ProviderName,
            input.StoreId,
            input.ProductId,
            input.ProductSkuId,
            input.Quantity,
            input.IncreaseSold
        );
    }

    public virtual async Task<bool> TryIncreaseInventoryAsync(IncreaseInventoryInput input)
    {
        await CheckPolicyAsync(ProductsPluginsFlashSalesPermissions.FlashSaleInventory.Increase);

        return await LocalFlashSaleInventoryReducer.TryIncreaseInventoryAsync(
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
