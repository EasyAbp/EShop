using EasyMall.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyMall.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class EasyMallController : AbpController
    {
        protected EasyMallController()
        {
            LocalizationResource = typeof(EasyMallResource);
        }
    }
}