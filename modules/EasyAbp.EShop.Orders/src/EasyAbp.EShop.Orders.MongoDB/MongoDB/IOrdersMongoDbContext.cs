using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Orders.MongoDB
{
    [ConnectionStringName(OrdersDbProperties.ConnectionStringName)]
    public interface IOrdersMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
