using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Orders.MongoDB
{
    [ConnectionStringName(OrdersDbProperties.ConnectionStringName)]
    public class OrdersMongoDbContext : AbpMongoDbContext, IOrdersMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureOrders();
        }
    }
}