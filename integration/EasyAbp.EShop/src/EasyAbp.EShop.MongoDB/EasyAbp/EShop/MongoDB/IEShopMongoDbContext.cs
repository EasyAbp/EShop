using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.MongoDB
{
    [ConnectionStringName(EShopDbProperties.ConnectionStringName)]
    public interface IEShopMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
