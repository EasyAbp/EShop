using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class ProductSkuIsNotFoundException : BusinessException
{
    public ProductSkuIsNotFoundException(Guid productSkuId) : base(FlashSalesErrorCodes.ProductSkuIsNotFound)
    {
        WithData(nameof(productSkuId), productSkuId);
    }
}
