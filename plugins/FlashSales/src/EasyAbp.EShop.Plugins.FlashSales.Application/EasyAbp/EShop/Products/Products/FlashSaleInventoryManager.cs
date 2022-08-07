using System;
using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products;

[ExposeServices(typeof(IFlashSaleInventoryManager), IncludeSelf = true)]
[Dependency(TryRegister = true)]
public class FlashSaleInventoryManager : IFlashSaleInventoryManager, ITransientDependency
{
    protected IFlashSaleInventoryAppService FlashSaleInventoryReducerAppService { get; }

    public FlashSaleInventoryManager(IFlashSaleInventoryAppService flashSaleInventoryReducerAppService)
    {
        FlashSaleInventoryReducerAppService = flashSaleInventoryReducerAppService;
    }

    public virtual async Task<bool> TryReduceInventoryAsync(Guid? tenantId, string providerName, Guid storeId,
        Guid productId, Guid productSkuId, int quantity, bool increaseSold)
    {
        return await FlashSaleInventoryReducerAppService.TryReduceAsync(new ReduceInventoryInput(
                tenantId, providerName, storeId, productId, productSkuId, quantity, increaseSold));
    }

    public virtual async Task<bool> TryRollBackInventoryAsync(Guid? tenantId, string providerName, Guid storeId,
        Guid productId, Guid productSkuId, int quantity, bool decreaseSold)
    {
        return await FlashSaleInventoryReducerAppService.TryIncreaseAsync(new IncreaseInventoryInput(
            tenantId, providerName, storeId, productId, productSkuId, quantity, decreaseSold));
    }
}