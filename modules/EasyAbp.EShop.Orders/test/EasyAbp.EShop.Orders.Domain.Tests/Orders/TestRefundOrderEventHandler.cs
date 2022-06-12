using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Orders.Orders;

public class TestRefundOrderEventHandler : IDistributedEventHandler<RefundOrderEto>, ITransientDependency
{
    public static RefundOrderEto LastEto { get; set; }

    public Task HandleEventAsync(RefundOrderEto eventData)
    {
        LastEto = eventData;

        return Task.CompletedTask;
    }
}