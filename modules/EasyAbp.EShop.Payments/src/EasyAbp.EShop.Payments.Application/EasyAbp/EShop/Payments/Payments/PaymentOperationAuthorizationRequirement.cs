using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public PaymentOperation PaymentOperation { get; }
        
        public PaymentOperationAuthorizationRequirement(PaymentOperation paymentOperation)
        {
            PaymentOperation = paymentOperation;
        }
    }
}