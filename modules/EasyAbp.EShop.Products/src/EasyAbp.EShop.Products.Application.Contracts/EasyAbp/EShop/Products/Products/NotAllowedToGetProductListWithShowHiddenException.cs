using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class NotAllowedToGetProductListWithShowHiddenException : BusinessException
    {
        public NotAllowedToGetProductListWithShowHiddenException() : base(ProductsErrorCodes.NotAllowedToGetProductListWithShowHidden)
        {
        }
    }
}