using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.StoreApproval.MongoDB
{
    [ConnectionStringName(StoreApprovalDbProperties.ConnectionStringName)]
    public class StoreApprovalMongoDbContext : AbpMongoDbContext, IStoreApprovalMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureStoreApproval();
        }
    }
}