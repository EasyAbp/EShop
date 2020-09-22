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
            if (resource.Orders.Any(order =>
                !order.ReducedInventoryAfterPlacingTime.HasValue ||
                order.PaymentId.HasValue ||
                order.PaidTime.HasValue))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (resource.Orders.Select(order => order.Currency).Distinct().Count() != 1)
            {
                // Todo: convert to a single currency.
                context.Fail();
                return Task.CompletedTask;
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}