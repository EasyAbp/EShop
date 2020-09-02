using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderCanceledEto
    {
        public OrderEto Order { get; set; }
    }
}