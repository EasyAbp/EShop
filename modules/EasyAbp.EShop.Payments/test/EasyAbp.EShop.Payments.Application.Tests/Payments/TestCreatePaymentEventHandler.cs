using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Payments.Payments
{
    public class TestCreatePaymentEventHandler : IDistributedEventHandler<CreatePaymentEto>, ISingletonDependency
    {
        public bool IsEventPublished { get; protected set; }

        public CreatePaymentEto CreatePaymentEto { get; protected set; }

        public Task HandleEventAsync(CreatePaymentEto eventData)
        {
            IsEventPublished = true;
            CreatePaymentEto = eventData;

            return Task.CompletedTask;
        }
    }
}