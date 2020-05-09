using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.WeChatPay.MongoDB
{
    [ConnectionStringName(WeChatPayDbProperties.ConnectionStringName)]
    public interface IWeChatPayMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
