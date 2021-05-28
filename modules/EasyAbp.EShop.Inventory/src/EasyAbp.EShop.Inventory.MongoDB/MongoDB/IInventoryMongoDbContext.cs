using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Inventory.MongoDB
{
    [ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
    public interface IInventoryMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
