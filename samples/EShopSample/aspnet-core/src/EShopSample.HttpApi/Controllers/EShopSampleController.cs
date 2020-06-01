using EShopSample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EShopSample.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EShopSampleController : AbpController
    {
        protected EShopSampleController()
        {
            LocalizationResource = typeof(EShopSampleResource);
        }
    }
}