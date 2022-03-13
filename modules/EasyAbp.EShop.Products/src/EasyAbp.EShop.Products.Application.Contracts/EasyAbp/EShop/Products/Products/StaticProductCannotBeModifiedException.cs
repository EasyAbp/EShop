using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class StaticProductCannotBeModifiedException : BusinessException
    {
        public StaticProductCannotBeModifiedException(Guid productId) : base(ProductsErrorCodes.StaticProductCannotBeModified)
        {
            WithData(nameof(productId), productId);
        }
    }
}