using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.MongoDB
{
    [ConnectionStringName(PaymentsDbProperties.ConnectionStringName)]
    public interface IPaymentsMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
