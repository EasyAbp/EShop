using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.MongoDB
{
    [ConnectionStringName(PluginsDbProperties.ConnectionStringName)]
    public class PluginsMongoDbContext : AbpMongoDbContext, IPluginsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEShopPlugins();
        }
    }
}