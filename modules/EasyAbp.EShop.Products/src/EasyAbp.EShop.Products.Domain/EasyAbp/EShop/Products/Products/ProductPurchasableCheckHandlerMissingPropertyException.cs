using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductPurchasableCheckHandlerMissingPropertyException : BusinessException
    {
        public ProductPurchasableCheckHandlerMissingPropertyException(string handlerName, string propertyKey) : base(
            message: $"The {handlerName} is missing extra property: {propertyKey}")
        {
        }
    }
}