using System;

namespace EasyAbp.EShop.Products.Products
{
    [Flags]
    public enum InventoryStrategy
    {
        NoNeed = 1,
        ReduceAfterPlacing = 2,
        ReduceAfterPayment = 4
    }
}