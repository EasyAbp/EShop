using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Promotions.MongoDB;

[ConnectionStringName(PromotionsDbProperties.ConnectionStringName)]
public class PromotionsMongoDbContext : AbpMongoDbContext, IPromotionsMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureEShopPluginsPromotions();
    }
}
