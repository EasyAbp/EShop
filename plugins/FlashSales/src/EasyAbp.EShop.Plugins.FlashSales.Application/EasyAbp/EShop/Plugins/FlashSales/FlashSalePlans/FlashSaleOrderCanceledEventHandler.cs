using System.Threading.Tasks;
using EasyAbp.Eshop.Products.Products;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCanceledEventHandler : IDistributedEventHandler<OrderCanceledEto>, ITransientDependency
{
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }

    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    protected IAbpApplication AbpApplication { get; }

    protected IFlashSaleInventoryManager FlashSaleInventoryManager { get; }

    protected IProductCache ProductCache { get; }

    protected IFlashSaleCurrentResultCache FlashSaleCurrentResultCache { get; }

    protected ILogger<FlashSaleOrderCanceledEventHandler> Logger { get; }

    public FlashSaleOrderCanceledEventHandler(
        IFlashSaleResultRepository flashSaleResultRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IAbpApplication abpApplication,
        IFlashSaleInventoryManager flashSaleInventoryManager,
        IProductCache productCache,
        IFlashSaleCurrentResultCache flashSaleCurrentResultCache,
        ILogger<FlashSaleOrderCanceledEventHandler> logger)
    {
        FlashSaleResultRepository = flashSaleResultRepository;
        UnitOfWorkManager = unitOfWorkManager;
        AbpApplication = abpApplication;
        FlashSaleInventoryManager = flashSaleInventoryManager;
        ProductCache = productCache;
        FlashSaleCurrentResultCache = flashSaleCurrentResultCache;
        Logger = logger;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(OrderCanceledEto eventData)
    {
        var flashSaleResult = await FlashSaleResultRepository
            .SingleOrDefaultAsync(x => x.Status != FlashSaleResultStatus.Failed && x.StoreId == eventData.Order.StoreId && x.OrderId == eventData.Order.Id);
        if (flashSaleResult == null)
        {
            return;
        }

        flashSaleResult.MarkAsFailed(FlashSaleResultFailedReason.OrderCanceled);

        await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);

        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            if (eventData.Order.OrderLines.Count == 0)
            {
                Logger.LogWarning("OrderCanceled order {orderId} orderLines is empty.", eventData.Order.Id);
                return;
            }
            var productId = eventData.Order.OrderLines[0].ProductId;
            var productSkuId = eventData.Order.OrderLines[0].ProductSkuId;
            var product = await ProductCache.GetAsync(productId);
            // try to roll back the inventory.
            if (!await FlashSaleInventoryManager.TryRollBackInventoryAsync(
                    eventData.TenantId, product.InventoryProviderName, eventData.Order.StoreId,
                    productId, productSkuId))
            {
                Logger.LogWarning("Failed to roll back the flash sale inventory.");
                return; // avoid to remove cache if the rollback failed.
            }

            // remove the cache so the user can try to order again.
            await FlashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
        });
    }
}
