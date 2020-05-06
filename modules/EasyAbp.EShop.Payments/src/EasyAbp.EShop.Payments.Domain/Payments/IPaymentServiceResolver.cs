using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentServiceResolver
    {
        Task<bool> TryRegisterProviderAsync(string paymentMethod, Type providerType);
        
        Task<List<string>> GetPaymentMethodsAsync();

        Task<Type> GetProviderTypeOrDefaultAsync(string paymentMethod);
    }
}