using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Serializable]
public class ExistRelatedFlashSalesResultsException : BusinessException
{
    public ExistRelatedFlashSalesResultsException(Guid planId) : base(FlashSalesErrorCodes.ExistRelatedFlashSalesResults)
    {
        WithData(nameof(planId), planId);
    }
}