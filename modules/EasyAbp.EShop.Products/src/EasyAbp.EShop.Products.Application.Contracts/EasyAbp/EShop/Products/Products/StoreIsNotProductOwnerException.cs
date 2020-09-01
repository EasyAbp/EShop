using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class StoreIsNotProductOwnerException : BusinessException
    {
        public StoreIsNotProductOwnerException(Guid productId, Guid storeId) : base(
            message: $"Store {storeId} is not a owner of the product {productId}")
        {
        }
    }
}