using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Payments.Payments
{
    public class BasicPayableCheckProvider : IPayableCheckProvider, ITransientDependency
    {
        public virtual Task CheckAsync(CreatePaymentDto input, List<OrderDto> orders,
            Dictionary<string, object> paymentExtraProperties)
        {
            foreach (var order in orders.Where(order => order.PaymentId.HasValue || order.PaidTime.HasValue))
            {
                throw new OrderPaymentAlreadyExistsException(order.Id);
            }

            if (orders.Select(order => order.Currency).Distinct().Count() != 1)
            {
                throw new MultiCurrencyNotSupportedException();
            }

            if (orders.Select(order => order.StoreId).Distinct().Count() != 1)
            {
                throw new MultiStorePaymentNotSupportedException();
            }

            return Task.CompletedTask;
        }
    }
}