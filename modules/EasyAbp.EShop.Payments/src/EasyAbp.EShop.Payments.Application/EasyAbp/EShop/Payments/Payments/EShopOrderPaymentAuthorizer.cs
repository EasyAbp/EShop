using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Payments.Payments
{
    public class EShopOrderPaymentAuthorizer : IPaymentAuthorizer, ITransientDependency
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrderAppService _orderAppService;

        public EShopOrderPaymentAuthorizer(
            ICurrentUser currentUser,
            IOrderAppService orderAppService)
        {
            _currentUser = currentUser;
            _orderAppService = orderAppService;
        }

        public virtual async Task<bool> IsPaymentItemAllowedAsync(Payment payment, PaymentItem paymentItem,
            Dictionary<string, object> inputExtraProperties)
        {
            if (paymentItem.ItemType != "EasyAbpEShopOrder")
            {
                return false;
            }

            var order = await _orderAppService.GetAsync(paymentItem.ItemKey);

            if (order.CustomerUserId != _currentUser.Id)
            {
                return false;
            }

            if (order.TotalPrice != paymentItem.OriginalPaymentAmount)
            {
                return false;
            }

            if (!order.ReducedInventoryAfterPlacingTime.HasValue)
            {
                return false;
            }

            var inputStoreIdString = inputExtraProperties.GetOrDefault("StoreId") as string;
            
            if (order.StoreId.ToString() != inputStoreIdString)
            {
                if (inputStoreIdString == null)
                {
                    inputExtraProperties.Add("StoreId", order.StoreId);
                }
                else
                {
                    return false;
                }
            }

            return order.OrderStatus == OrderStatus.Pending;
        }
    }
}