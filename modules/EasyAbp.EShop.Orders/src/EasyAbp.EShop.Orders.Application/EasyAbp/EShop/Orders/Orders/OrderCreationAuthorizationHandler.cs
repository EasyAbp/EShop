using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Orders.Orders
{
    public abstract class OrderCreationAuthorizationHandler : AuthorizationHandler<OrderOperationAuthorizationRequirement, OrderCreationResource>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OrderOperationAuthorizationRequirement requirement,
            OrderCreationResource resource)
        {
            if (requirement.OrderOperation != OrderOperation.Creation)
            {
                return;
            }
            
            await HandleOrderCreationAsync(context, requirement, resource);
        }

        protected abstract Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource);
    }
}