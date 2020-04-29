using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class NotAllowedToGetProductListWithShowHiddenException : BusinessException
    {
        public NotAllowedToGetProductListWithShowHiddenException() : base(
            message: $"You have no permission to get product list with hidden products.")
        {
        }
    }
}