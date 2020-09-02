using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentChecker : IOrderPaymentChecker, ITransientDependency
    {
        public virtual async Task<bool> IsValidPaymentAsync(Order order, EShopPaymentEto payment, EShopPaymentItemEto paymentItem)
        {
            return await IsStoreIdCorrectAsync(order, paymentItem) &&
                   await IsPaymentPriceCorrectAsync(order, paymentItem) &&
                   await IsUserAllowedToPayAsync(order, payment);
        }
        
        protected virtual Task<bool> IsStoreIdCorrectAsync(Order order, EShopPaymentItemEto paymentItem)
        {
            return Task.FromResult(
                Guid.TryParse(paymentItem.GetProperty<string>("StoreId"), out var paymentStoreId) &&
                order.StoreId == paymentStoreId);
        }
        
        protected virtual Task<bool> IsPaymentPriceCorrectAsync(Order order, EShopPaymentItemEto paymentItem)
        {
            return Task.FromResult(order.ActualTotalPrice == paymentItem.OriginalPaymentAmount);
        }

        protected virtual Task<bool> IsUserAllowedToPayAsync(Order order, EShopPaymentEto payment)
        {
            return Task.FromResult(order.CustomerUserId == payment.UserId);
        }
    }
}