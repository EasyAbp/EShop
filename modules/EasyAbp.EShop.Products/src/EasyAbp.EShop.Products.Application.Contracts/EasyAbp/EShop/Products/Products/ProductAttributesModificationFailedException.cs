using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributesModificationFailedException : BusinessException
    {
        public ProductAttributesModificationFailedException() : base(
            message: "Should ensure SKUs are empty if you want to modify attributes of a product.")
        {
        }
    }
}