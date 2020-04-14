using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Payment.MongoDB
{
    [ConnectionStringName(PaymentDbProperties.ConnectionStringName)]
    public class PaymentMongoDbContext : AbpMongoDbContext, IPaymentMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigurePayment();
        }
    }
}