using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Flags]
    public enum OrderStatus
    {
        Pending = 1,
        Processing = 2,
        Completed = 4,
        Canceled = 8
    }
}