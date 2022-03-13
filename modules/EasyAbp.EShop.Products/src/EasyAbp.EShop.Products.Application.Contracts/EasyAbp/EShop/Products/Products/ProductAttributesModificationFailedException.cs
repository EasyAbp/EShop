using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributesModificationFailedException : BusinessException
    {
        public ProductAttributesModificationFailedException() : base(ProductsErrorCodes.ProductAttributesModificationFailed)
        {
        }
    }
}