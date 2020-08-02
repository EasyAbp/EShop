using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    public abstract class StoreApprovalController : AbpController
    {
        protected StoreApprovalController()
        {
            LocalizationResource = typeof(StoreApprovalResource);
        }
    }
}
