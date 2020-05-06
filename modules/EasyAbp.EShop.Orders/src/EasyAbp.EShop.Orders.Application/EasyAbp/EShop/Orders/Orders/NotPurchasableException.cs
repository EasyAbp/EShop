using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class NotPurchasableException : BusinessException
    {
        public NotPurchasableException(Guid productId, Guid? productSkuId, string reason) : base(
            message: $"Product {productId} (SKU: {productSkuId}) cannot be purchased, the reason is: {reason}")
        {
        }
    }
}