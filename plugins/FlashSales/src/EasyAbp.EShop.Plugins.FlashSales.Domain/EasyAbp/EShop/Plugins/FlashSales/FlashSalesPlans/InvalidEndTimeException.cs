using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class InvalidEndTimeException : BusinessException
{
    public InvalidEndTimeException()
        : base(FlashSalesErrorCodes.InvalidEndTime)
    {

    }
}
