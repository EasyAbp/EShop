using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class RelatedFlashSaleResultsExistException : BusinessException
{
    public RelatedFlashSaleResultsExistException(Guid planId) : base(FlashSalesErrorCodes.RelatedFlashSaleResultsExist)
    {
        WithData(nameof(planId), planId);
    }
}