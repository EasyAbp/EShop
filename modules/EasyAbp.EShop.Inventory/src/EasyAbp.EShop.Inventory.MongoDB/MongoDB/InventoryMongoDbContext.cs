using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Inventory.MongoDB
{
    [ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
    public class InventoryMongoDbContext : AbpMongoDbContext, IInventoryMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureInventory();
        }
    }
}