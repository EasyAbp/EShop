using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

internal class EndTimeMustBeLaterThanBeginTimeException : BusinessException
{
    public EndTimeMustBeLaterThanBeginTimeException()
        : base(FlashSalesErrorCodes.EndTimeMustBeLaterThanBeginTime)
    {

    }
}
