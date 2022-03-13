using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDetailHasBeenUsedException : BusinessException
    {
        public ProductDetailHasBeenUsedException(Guid productDetailId) : base(ProductsErrorCodes.ProductDetailHasBeenUsed)
        {
            WithData(nameof(productDetailId), productDetailId);
        }
    }
}