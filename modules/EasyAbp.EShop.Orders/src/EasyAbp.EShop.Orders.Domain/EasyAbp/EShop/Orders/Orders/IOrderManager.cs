using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderManager : IDomainService
    {
        Task<Order> CompleteAsync(Order order);
        
        Task<Order> CancelAsync(Order order, string cancellationReason);
    }
}