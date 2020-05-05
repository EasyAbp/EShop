using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Orders.Orders;

namespace EasyAbp.EShop.Orders.EntityFrameworkCore
{
    [ConnectionStringName(OrdersDbProperties.ConnectionStringName)]
    public class OrdersDbContext : AbpDbContext<OrdersDbContext>, IOrdersDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureOrders();
        }
    }
}
