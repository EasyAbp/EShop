using System;
using System.Linq;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderRepository : EfCoreRepository<OrdersDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<OrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<Order> WithDetails()
        {
            return base.WithDetails().Include(x => x.OrderLines);
        }
    }
}