using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Stores.MongoDB
{
    [ConnectionStringName(StoresDbProperties.ConnectionStringName)]
    public interface IStoresMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
