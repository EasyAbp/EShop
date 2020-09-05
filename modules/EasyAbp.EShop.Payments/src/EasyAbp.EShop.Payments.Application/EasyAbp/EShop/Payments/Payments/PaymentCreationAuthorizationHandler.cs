using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Payments.Payments
{
    public abstract class PaymentCreationAuthorizationHandler : AuthorizationHandler<PaymentOperationAuthorizationRequirement, PaymentCreationResource>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PaymentOperationAuthorizationRequirement requirement,
            PaymentCreationResource resource)
        {
            if (requirement.PaymentOperation != PaymentOperation.Creation)
            {
                return;
            }
            
            await HandlePaymentCreationAsync(context, requirement, resource);
        }

        protected abstract Task HandlePaymentCreationAsync(AuthorizationHandlerContext context,
            PaymentOperationAuthorizationRequirement requirement, PaymentCreationResource resource);
    }
}