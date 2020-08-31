using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class BasicOrderCompletableCheckProvider : IOrderCompletableCheckProvider, ITransientDependency
    {
        public virtual Task CheckAsync(Order order)
        {
            if (!order.PaidTime.HasValue || !order.ReducedInventoryAfterPaymentTime.HasValue)
            {
                throw new OrderIsInWrongStageException(order.Id);
            }
            
            return Task.CompletedTask;
        }
    }
}