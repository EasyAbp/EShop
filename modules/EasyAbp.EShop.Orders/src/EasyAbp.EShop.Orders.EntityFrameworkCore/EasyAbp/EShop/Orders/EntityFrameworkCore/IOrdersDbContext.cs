using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Orders.Orders;

namespace EasyAbp.EShop.Orders.EntityFrameworkCore
{
    [ConnectionStringName(OrdersDbProperties.ConnectionStringName)]
    public interface IOrdersDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Order> Orders { get; set; }
    }
}
