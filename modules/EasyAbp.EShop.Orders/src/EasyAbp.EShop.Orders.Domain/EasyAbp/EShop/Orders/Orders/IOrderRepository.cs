using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}