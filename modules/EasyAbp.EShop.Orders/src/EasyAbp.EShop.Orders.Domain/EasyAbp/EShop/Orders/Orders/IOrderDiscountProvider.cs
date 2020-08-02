using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderDiscountProvider
    {
        Task<Order> DiscountAsync(Order order);
    }
}