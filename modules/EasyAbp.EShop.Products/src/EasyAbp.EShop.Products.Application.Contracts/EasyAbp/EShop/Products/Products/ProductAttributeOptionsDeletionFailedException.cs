using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOptionsDeletionFailedException : BusinessException
    {
        public ProductAttributeOptionsDeletionFailedException() : base(
            message: "Should ensure there are no SKUs using the attribute option which you want to delete.")
        {
        }
    }
}