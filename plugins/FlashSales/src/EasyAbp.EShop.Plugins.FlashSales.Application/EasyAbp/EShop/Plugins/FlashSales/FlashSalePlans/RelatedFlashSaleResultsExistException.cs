using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Serializable]
public class RelatedFlashSaleResultsExistException : BusinessException
{
    public RelatedFlashSaleResultsExistException(Guid planId) : base(FlashSalesErrorCodes.RelatedFlashSaleResultsExist)
    {
        WithData(nameof(planId), planId);
    }
}