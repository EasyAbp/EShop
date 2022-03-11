using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOptionsDeletionFailedException : BusinessException
    {
        public ProductAttributeOptionsDeletionFailedException() : base(ProductsErrorCodes.ProductAttributeOptionsDeletionFailed)
        {
        }
    }
}