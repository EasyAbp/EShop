using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class StoreIsNotProductOwnerException : BusinessException
    {
        public StoreIsNotProductOwnerException(Guid productId, Guid storeId) : base(ProductsErrorCodes.StoreIsNotProductOwner)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(storeId), storeId);
        }
    }
}