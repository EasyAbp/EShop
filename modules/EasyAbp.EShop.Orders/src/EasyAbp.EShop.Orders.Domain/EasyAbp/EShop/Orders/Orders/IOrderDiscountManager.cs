using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderDiscountManager : IDomainService
    {
        Task<Order> DiscountAsync(Order order, Dictionary<string, object> inputExtraProperties);
    }
}