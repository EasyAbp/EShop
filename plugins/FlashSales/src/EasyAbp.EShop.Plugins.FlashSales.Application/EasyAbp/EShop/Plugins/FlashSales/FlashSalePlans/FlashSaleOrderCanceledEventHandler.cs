using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class FlashSaleOrderCanceledEventHandler : IDistributedEventHandler<OrderCanceledEto>, ITransientDependency
{
    protected IServiceScopeFactory ServiceScopeFactory { get; }
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }
    protected IUnitOfWorkManager UnitOfWorkManager { get; }

    public FlashSaleOrderCanceledEventHandler(
        IServiceScopeFactory serviceScopeFactory,
        IFlashSaleResultRepository flashSaleResultRepository,
        IUnitOfWorkManager unitOfWorkManager)
    {
        ServiceScopeFactory = serviceScopeFactory;
        FlashSaleResultRepository = flashSaleResultRepository;
        UnitOfWorkManager = unitOfWorkManager;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(OrderCanceledEto eventData)
    {
        var flashSaleResult = await FlashSaleResultRepository
            .SingleOrDefaultAsync(x =>
                x.Status != FlashSaleResultStatus.Failed && x.StoreId == eventData.Order.StoreId &&
                x.OrderId == eventData.Order.Id);
        if (flashSaleResult == null)
        {
            return;
        }

        flashSaleResult.MarkAsFailed(FlashSaleResultFailedReason.OrderCanceled);

        await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);

        UnitOfWorkManager.Current.OnCompleted(async () =>
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var flashSaleCurrentResultCache = scope.ServiceProvider.GetRequiredService<IFlashSaleCurrentResultCache>();
            // remove the cache so the user can try to order again.
            await flashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
        });
    }
}