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
        
        public virtual Task<bool> TryRegisterProviderAsync(string paymentMethod, Type providerType)
        {
            if (Providers.ContainsKey(paymentMethod))
            {
                return Task.FromResult(false);
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                if (scope.ServiceProvider.GetService(providerType) == null)
                {
                    return Task.FromResult(false);
                }
            }

            Providers.Add(paymentMethod, providerType);

            return Task.FromResult(true);
        }

        public virtual Task<List<string>> GetPaymentMethodsAsync()
        {
            return Task.FromResult(Providers.Keys.ToList());
        }

        public virtual Task<Type> GetProviderTypeOrDefaultAsync(string paymentMethod)
        {
            return Task.FromResult(Providers.GetOrDefault(paymentMethod));
        }
    }
}