using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderOperationAuthorizationRequirement : IAuthorizationRequirement
    {
        public OrderOperation OrderOperation { get; }
        
        public OrderOperationAuthorizationRequirement(OrderOperation orderOperation)
        {
            OrderOperation = orderOperation;
        }
    }
}