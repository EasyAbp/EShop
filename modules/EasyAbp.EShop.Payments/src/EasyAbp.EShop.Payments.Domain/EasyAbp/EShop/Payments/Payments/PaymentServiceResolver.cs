using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentServiceResolver : IPaymentServiceResolver, ISingletonDependency
    {
        protected readonly Dictionary<string, Type> Providers = new Dictionary<string, Type>();
        
        private readonly IServiceProvider _serviceProvider;

        public PaymentServiceResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public virtual bool TryRegisterProvider(string paymentMethod, Type providerType)
        {
            if (Providers.ContainsKey(paymentMethod))
            {
                return false;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                if (scope.ServiceProvider.GetService(providerType) == null)
                {
                    return false;
                }
            }

            Providers.Add(paymentMethod, providerType);

            return true;
        }

        public virtual List<string> GetPaymentMethods()
        {
            return Providers.Keys.ToList();
        }

        public virtual Type GetProviderTypeOrDefault(string paymentMethod)
        {
            return Providers.GetOrDefault(paymentMethod);
        }
    }
}