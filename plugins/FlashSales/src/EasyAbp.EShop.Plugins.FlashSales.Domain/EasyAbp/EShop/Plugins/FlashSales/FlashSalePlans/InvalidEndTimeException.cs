using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class InvalidEndTimeException : BusinessException
{
    public InvalidEndTimeException()
        : base(FlashSalesErrorCodes.InvalidEndTime)
    {

    }
}
