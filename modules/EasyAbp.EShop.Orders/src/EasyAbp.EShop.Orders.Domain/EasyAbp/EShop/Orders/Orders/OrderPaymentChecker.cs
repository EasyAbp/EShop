using System;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentChecker : IOrderPaymentChecker, ITransientDependency
    {
        public virtual async Task<bool> IsValidPaymentAsync(Order order, PaymentEto payment, PaymentItemEto paymentItem)
        {
            return await IsStoreIdCorrectAsync(order, payment) &&
                   await IsPaymentPriceCorrectAsync(order, paymentItem) &&
                   await IsUserAllowedToPayAsync(order, payment);
        }
        
        protected virtual Task<bool> IsStoreIdCorrectAsync(Order order, PaymentEto payment)
        {
            return Task.FromResult(Guid.TryParse(payment.GetProperty<string>("StoreId"), out var paymentStoreId) &&
                                   order.StoreId == paymentStoreId);
        }
        
        protected virtual Task<bool> IsPaymentPriceCorrectAsync(Order order, PaymentItemEto paymentItem)
        {
            return Task.FromResult(order.TotalPrice == paymentItem.OriginalPaymentAmount);
        }

        protected virtual Task<bool> IsUserAllowedToPayAsync(Order order, PaymentEto payment)
        {
            return Task.FromResult(order.CustomerUserId == payment.UserId);
        }
    }
}