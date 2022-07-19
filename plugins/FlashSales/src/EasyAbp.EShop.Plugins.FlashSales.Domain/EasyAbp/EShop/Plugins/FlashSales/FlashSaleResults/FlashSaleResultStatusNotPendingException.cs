using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class FlashSaleResultStatusNotPendingException : BusinessException
{
    public FlashSaleResultStatusNotPendingException(Guid resultId) : base(FlashSalesErrorCodes.FlashSaleResultStatusNotPending)
    {
        WithData(nameof(resultId), resultId);
    }
}
