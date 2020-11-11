using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Flags]
    public enum OrderOperation
    {
        Creation = 1,
        Cancellation = 2
    }
}