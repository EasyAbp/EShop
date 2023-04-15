using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Promotions.MongoDB;

[ConnectionStringName(PromotionsDbProperties.ConnectionStringName)]
public interface IPromotionsMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
