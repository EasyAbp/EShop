using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentServiceResolver
    {
        bool TryRegisterProvider(string paymentMethod, Type providerType);
        
        List<string> GetPaymentMethods();

        Type GetProviderTypeOrDefault(string paymentMethod);
    }
}