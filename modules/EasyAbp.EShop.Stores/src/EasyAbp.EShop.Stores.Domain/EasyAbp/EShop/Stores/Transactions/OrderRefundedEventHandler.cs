using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class OrderRefundedEventHandler : IDistributedEventHandler<OrderRefundedEto>, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ITransactionRepository _transactionRepository;

        public OrderRefundedEventHandler(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ITransactionRepository transactionRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _transactionRepository = transactionRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(OrderRefundedEto eventData)
        {
            var order = eventData.Order;
            var refund = eventData.Refund;

            // Record the transaction only if the order is completed.
            if (!order.CompletionTime.HasValue)
            {
                return;
            }
            
            using var changeTenant = _currentTenant.Change(order.TenantId);

            var transaction = new Transaction(_guidGenerator.Create(), order.TenantId, order.StoreId, order.Id,
                TransactionType.Credit, StoresConsts.TransactionOrderRefundedActionName, refund.Currency,
                -refund.RefundAmount);

            await _transactionRepository.InsertAsync(transaction, true);
        }
    }
}