using EasyAbp.EShop.Payment.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Payment.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class PaymentPageModel : AbpPageModel
    {
        protected PaymentPageModel()
        {
            LocalizationResourceType = typeof(PaymentResource);
            ObjectMapperContext = typeof(EShopPaymentWebModule);
        }
    }
}