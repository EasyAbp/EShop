using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Stores.Transactions
{
    public class OrderCompletedEventHandler : IDistributedEventHandler<OrderCompletedEto>, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ITransactionRepository _transactionRepository;

        public OrderCompletedEventHandler(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ITransactionRepository transactionRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _transactionRepository = transactionRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(OrderCompletedEto eventData)
        {
            var order = eventData.Order;
            
            using var changeTenant = _currentTenant.Change(order.TenantId);

            var transaction = new Transaction(_guidGenerator.Create(), order.TenantId, order.StoreId, order.Id,
                TransactionType.Debit, StoresConsts.TransactionOrderCompletedActionName, order.Currency,
                order.ActualTotalPrice);

            await _transactionRepository.InsertAsync(transaction, true);
        }
    }
}