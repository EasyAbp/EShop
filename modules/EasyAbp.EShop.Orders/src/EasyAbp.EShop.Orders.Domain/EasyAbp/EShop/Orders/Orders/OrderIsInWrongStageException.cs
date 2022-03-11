using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderIsInWrongStageException : BusinessException
    {
        public OrderIsInWrongStageException(Guid orderId) : base(OrdersErrorCodes.OrderIsInWrongStage)
        {
            WithData(nameof(orderId), orderId);
        }
    }
}