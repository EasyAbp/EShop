using EasyAbp.EShop.Payments.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments
{
    [Area(EShopPaymentsRemoteServiceConsts.ModuleName)]
    public abstract class PaymentsController : AbpControllerBase
    {
        protected PaymentsController()
        {
            LocalizationResource = typeof(PaymentsResource);
        }
    }
}
