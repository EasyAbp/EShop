using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderCompletedEto
    {
        public OrderEto Order { get; set; }
    }
}