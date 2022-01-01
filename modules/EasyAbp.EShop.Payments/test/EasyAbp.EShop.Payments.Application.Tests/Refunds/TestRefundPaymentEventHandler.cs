using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class TestRefundPaymentEventHandler : IDistributedEventHandler<RefundPaymentEto>, ISingletonDependency
    {
        public bool IsEventPublished { get; protected set; }
        
        public Task HandleEventAsync(RefundPaymentEto eventData)
        {
            IsEventPublished = true;
            
            return Task.CompletedTask;
        }
    }
}