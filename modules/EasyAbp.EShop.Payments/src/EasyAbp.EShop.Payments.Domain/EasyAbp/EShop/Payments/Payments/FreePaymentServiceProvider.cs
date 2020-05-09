using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Payments.Payments
{
    public class FreePaymentServiceProvider : IPaymentServiceProvider, ITransientDependency
    {
        private readonly IClock _clock;
        private readonly IPaymentRepository _paymentRepository;
        public const string PaymentMethod = "Free";
        
        public FreePaymentServiceProvider(
            IClock clock,
            IPaymentRepository paymentRepository)
        {
            _clock = clock;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> PayAsync(Payment payment, Dictionary<string, object> inputExtraProperties,
            Dictionary<string, object> payeeConfigurations)
        {
            payment.SetPayeeAccount("None");
            
            payment.SetExternalTradingCode(payment.Id.ToString());

            payment.CompletePayment(_clock.Now);

            return await _paymentRepository.UpdateAsync(payment, true);
        }
    }
}