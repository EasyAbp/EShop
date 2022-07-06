using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class ProductIsNotInThisStoreException : BusinessException
{
    public ProductIsNotInThisStoreException(Guid productId, Guid storeId) : base(FlashSalesErrorCodes.ProductIsNotInThisStore)
    {
        WithData(nameof(productId), productId);
        WithData(nameof(storeId), storeId);
    }
}
