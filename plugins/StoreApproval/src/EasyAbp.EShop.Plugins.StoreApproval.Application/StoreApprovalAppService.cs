using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    public abstract class StoreApprovalAppService : ApplicationService
    {
        protected StoreApprovalAppService()
        {
            LocalizationResource = typeof(StoreApprovalResource);
            ObjectMapperContext = typeof(StoreApprovalApplicationModule);
        }
    }
}
