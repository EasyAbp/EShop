using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderIsInWrongStageException : BusinessException
    {
        public OrderIsInWrongStageException(Guid orderId) : base(message: $"The order {orderId} is in the wrong stage.")
        {
        }
    }
}