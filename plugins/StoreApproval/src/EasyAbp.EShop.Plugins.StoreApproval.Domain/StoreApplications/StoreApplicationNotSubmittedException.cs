using Volo.Abp;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplicationNotSubmittedException : BusinessException
    {
        public StoreApplicationNotSubmittedException()
            : base(StoreApprovalErrorCodes.NotSubmitted)
        {

        }
    }
}