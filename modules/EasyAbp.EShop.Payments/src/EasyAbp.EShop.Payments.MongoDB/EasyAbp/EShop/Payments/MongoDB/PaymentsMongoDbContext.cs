using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payments.MongoDB
{
    [ConnectionStringName(PaymentsDbProperties.ConnectionStringName)]
    public class PaymentsMongoDbContext : AbpMongoDbContext, IPaymentsMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEShopPayments();
        }
    }
}