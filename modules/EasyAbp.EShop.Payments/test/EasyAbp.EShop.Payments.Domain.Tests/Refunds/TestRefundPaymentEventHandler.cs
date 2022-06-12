using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class TestRefundPaymentEventHandler : IDistributedEventHandler<RefundPaymentEto>, ITransientDependency
    {
        public static RefundPaymentEto LastEto { get; set; }

        public Task HandleEventAsync(RefundPaymentEto eventData)
        {
            LastEto = eventData;

            return Task.CompletedTask;
        }
    }
}