using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Microsoft.Extensions.DependencyInjection;
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

    public FlashSaleOrderCanceledEventHandler(
        IFlashSaleResultRepository flashSaleResultRepository,
        IUnitOfWorkManager unitOfWorkManager,
        IAbpApplication abpApplication)
    {
        FlashSaleResultRepository = flashSaleResultRepository;
        UnitOfWorkManager = unitOfWorkManager;
        AbpApplication = abpApplication;
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
            using var scope = AbpApplication.ServiceProvider.CreateScope();
            var flashSaleCurrentResultCache = scope.ServiceProvider.GetRequiredService<IFlashSaleCurrentResultCache>();
            // remove the cache so the user can try to order again.
            await flashSaleCurrentResultCache.RemoveAsync(flashSaleResult.PlanId, flashSaleResult.UserId);
        });
    }
}
