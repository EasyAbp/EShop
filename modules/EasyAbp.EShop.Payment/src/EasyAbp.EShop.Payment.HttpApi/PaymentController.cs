using EasyAbp.EShop.Payment.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payment
{
    public abstract class PaymentController : AbpController
    {
        protected PaymentController()
        {
            LocalizationResource = typeof(PaymentResource);
        }
    }
}
