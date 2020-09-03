using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Payments.Payments
{
    public class BasicPaymentCreationAuthorizationHandler : PaymentCreationAuthorizationHandler
    {

        public BasicPaymentCreationAuthorizationHandler()
        {
        }
        
        protected override Task HandlePaymentCreationAsync(AuthorizationHandlerContext context,
            PaymentOperationAuthorizationRequirement requirement, PaymentCreationResource resource)
        {
            if (resource.Orders.Any(order => order.PaymentId.HasValue || order.PaidTime.HasValue))
            {
                context.Fail();
            }

            if (resource.Orders.Select(order => order.Currency).Distinct().Count() != 1)
            {
                // Todo: convert to a single currency.
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}