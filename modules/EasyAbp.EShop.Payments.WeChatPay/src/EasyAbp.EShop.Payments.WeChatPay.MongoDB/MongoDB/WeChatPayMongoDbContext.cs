using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.WeChatPay.MongoDB
{
    [ConnectionStringName(WeChatPayDbProperties.ConnectionStringName)]
    public class WeChatPayMongoDbContext : AbpMongoDbContext, IWeChatPayMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureWeChatPay();
        }
    }
}