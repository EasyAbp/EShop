using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.MongoDB
{
    [ConnectionStringName(EShopDbProperties.ConnectionStringName)]
    public class EShopMongoDbContext : AbpMongoDbContext, IEShopMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEShop();
        }
    }
}