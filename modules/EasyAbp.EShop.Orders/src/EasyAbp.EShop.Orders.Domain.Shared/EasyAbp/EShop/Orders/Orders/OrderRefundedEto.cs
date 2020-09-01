using System;
using EasyAbp.EShop.Payments.Refunds;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderRefundedEto
    {
        public OrderEto Order { get; set; }
        
        public EShopRefundEto Refund { get; set; }
    }
}