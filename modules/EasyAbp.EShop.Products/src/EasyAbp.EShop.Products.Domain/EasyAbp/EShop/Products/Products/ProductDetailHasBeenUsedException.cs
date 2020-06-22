using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDetailHasBeenUsedException : BusinessException
    {
        public ProductDetailHasBeenUsedException(Guid productDetailId) : base(
            message: $"ProductDetail {productDetailId} has been used.")
        {
        }
    }
}