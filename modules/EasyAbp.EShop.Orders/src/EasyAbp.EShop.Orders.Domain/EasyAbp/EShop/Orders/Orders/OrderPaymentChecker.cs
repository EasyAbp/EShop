using System;
using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderPaymentChecker : IOrderPaymentChecker, ITransientDependency
    {
        public Task<bool> IsValidPaymentAsync(Order order, PaymentEto paymentEto)
        {
            return Task.FromResult(
                Guid.TryParse(paymentEto.GetProperty<string>("StoreId"), out var paymentStoreId) &&
                order.StoreId == paymentStoreId
            );
        }
    }
}