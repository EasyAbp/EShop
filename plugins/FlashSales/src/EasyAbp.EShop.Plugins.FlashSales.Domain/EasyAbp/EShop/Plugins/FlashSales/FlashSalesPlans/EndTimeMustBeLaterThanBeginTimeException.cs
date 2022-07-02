using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class EndTimeMustBeLaterThanBeginTimeException : BusinessException
{
    public EndTimeMustBeLaterThanBeginTimeException()
        : base(FlashSalesErrorCodes.EndTimeMustBeLaterThanBeginTime)
    {

    }
}
