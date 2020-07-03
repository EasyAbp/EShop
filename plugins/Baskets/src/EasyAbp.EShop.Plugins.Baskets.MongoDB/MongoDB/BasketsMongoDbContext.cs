using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Baskets.MongoDB
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public class BasketsMongoDbContext : AbpMongoDbContext, IBasketsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureBaskets();
        }
    }
}