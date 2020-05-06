using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments
{
    public abstract class PaymentsController : AbpController
    {
        protected PaymentsController()
        {
            LocalizationResource = typeof(PaymentsResource);
        }
    }
}
