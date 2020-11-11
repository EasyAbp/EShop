using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Orders.Orders
{
    public abstract class OrderCancellationAuthorizationHandler : AuthorizationHandler<OrderOperationAuthorizationRequirement, Order>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderOperationAuthorizationRequirement requirement,
            Order resource)
        {
            if (requirement.OrderOperation != OrderOperation.Cancellation)
            {
                return;
            }
            
            await HandleOrderCancellationAsync(context, requirement, resource);
        }

        protected abstract Task HandleOrderCancellationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, Order resource);
    }
}