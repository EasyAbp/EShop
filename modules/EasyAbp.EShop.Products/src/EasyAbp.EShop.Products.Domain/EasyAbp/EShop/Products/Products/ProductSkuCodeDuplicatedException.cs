using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductSkuCodeDuplicatedException : BusinessException
    {
        public ProductSkuCodeDuplicatedException(Guid productId, string code) : base(
            message: $"Sku code {code} is duplicate for the product {productId}")
        {
        }
    }
}