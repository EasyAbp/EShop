using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Payments
{
    [Dependency(ServiceLifetime.Transient, TryRegister = true)]
    public class FreePaymentServiceProvider : IPaymentServiceProvider
    {
        public async Task<Payment> PayForOrderAsync(Payment payment, Dictionary<string, object> extraProperties = null)
        {
            throw new System.NotImplementedException();
        }
    }
}