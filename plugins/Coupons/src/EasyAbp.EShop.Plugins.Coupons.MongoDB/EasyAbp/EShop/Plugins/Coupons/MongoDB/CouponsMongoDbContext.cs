using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public class CouponsMongoDbContext : AbpMongoDbContext, ICouponsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEShopPluginsCoupons();
        }
    }
}