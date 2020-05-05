using System;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderRepository : EfCoreRepository<OrdersDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}