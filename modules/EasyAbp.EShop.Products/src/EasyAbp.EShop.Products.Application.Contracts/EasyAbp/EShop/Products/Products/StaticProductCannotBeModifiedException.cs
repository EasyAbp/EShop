using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class StaticProductCannotBeModifiedException : BusinessException
    {
        public StaticProductCannotBeModifiedException(Guid productId) : base(
            message: $"Cannot modify the static product: {productId}")
        {
        }
    }
}