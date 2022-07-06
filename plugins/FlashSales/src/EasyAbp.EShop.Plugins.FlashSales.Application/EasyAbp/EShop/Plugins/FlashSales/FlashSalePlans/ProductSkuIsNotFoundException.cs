using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class ProductSkuIsNotFoundException : BusinessException
{
    public ProductSkuIsNotFoundException(Guid productSkuId) : base(FlashSalesErrorCodes.ProductSkuIsNotFound)
    {
        WithData(nameof(productSkuId), productSkuId);
    }
}
