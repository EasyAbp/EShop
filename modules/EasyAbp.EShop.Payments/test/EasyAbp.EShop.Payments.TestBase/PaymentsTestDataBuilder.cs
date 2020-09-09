using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Payments
{
    public class PaymentsTestDataBuilder : ITransientDependency
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsTestDataBuilder(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public void Build()
        {
            AsyncHelper.RunSync(BuildAsync);
        }

        public async Task BuildAsync()
        {
            // TODO: How to create payment?
            // await _paymentRepository.InsertAsync(new Payment());
        }
    }
}