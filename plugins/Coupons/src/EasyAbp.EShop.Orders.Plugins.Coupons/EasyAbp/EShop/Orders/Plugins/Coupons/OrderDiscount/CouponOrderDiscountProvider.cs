using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount
{
    public class CouponOrderDiscountProvider : IOrderDiscountProvider, ITransientDependency
    {
        public async Task<Order> DiscountAsync(Order order, Dictionary<string, object> inputExtraProperties)
        {
            throw new System.NotImplementedException();
        }
    }
}