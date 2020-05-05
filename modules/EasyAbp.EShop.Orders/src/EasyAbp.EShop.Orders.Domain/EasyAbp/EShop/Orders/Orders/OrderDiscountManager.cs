using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderDiscountManager : DomainService, IOrderDiscountManager
    {
        public async Task<Order> DiscountAsync(Order order, Dictionary<string, object> inputExtraProperties)
        {
            foreach (var provider in ServiceProvider.GetServices<IOrderDiscountProvider>())
            {
                await provider.DiscountAsync(order, inputExtraProperties);
            }

            return order;
        }
    }
}