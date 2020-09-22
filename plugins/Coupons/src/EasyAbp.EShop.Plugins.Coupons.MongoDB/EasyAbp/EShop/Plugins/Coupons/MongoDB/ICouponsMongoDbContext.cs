using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public interface ICouponsMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
