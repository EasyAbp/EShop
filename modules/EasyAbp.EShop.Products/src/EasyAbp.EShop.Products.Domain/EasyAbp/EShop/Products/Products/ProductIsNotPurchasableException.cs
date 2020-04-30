using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductIsNotPurchasableException : BusinessException
    {
        public ProductIsNotPurchasableException(Guid id, string reason) : base(
            message: $"Product {id} cannot be purchased, the reason is: {reason}")
        {
        }
    }
}