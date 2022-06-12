using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Orders.Orders
{
    public class UnpaidOrderAutoCancelJob : AsyncBackgroundJob<UnpaidOrderAutoCancelArgs>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public UnpaidOrderAutoCancelJob(
            ICurrentTenant currentTenant,
            IOrderManager orderManager,
            IOrderRepository orderRepository,
            IDistributedEventBus distributedEventBus)
        {
            _currentTenant = currentTenant;
            _orderManager = orderManager;
            _orderRepository = orderRepository;
            _distributedEventBus = distributedEventBus;
        }
        
        [UnitOfWork(true)]
        public override async Task ExecuteAsync(UnpaidOrderAutoCancelArgs args)
        {
            using var changeTenant = _currentTenant.Change(args.TenantId);
            
            var order = await _orderRepository.FindAsync(args.OrderId);

            if (order is not {CanceledTime: null} || order.IsPaid())
            {
                return;
            }

            if (order.IsInPayment())
            {
                // Cancel the payment and then cancel the order in EasyAbp.EShop.Orders.Orders.PaymentCanceledEventHandler
                await _distributedEventBus.PublishAsync(new CancelPaymentEto(args.TenantId, order.PaymentId!.Value));
            }
            else
            {
                await _orderManager.CancelAsync(order, OrdersConsts.UnpaidAutoCancellationReason);
            }
        }
    }
}