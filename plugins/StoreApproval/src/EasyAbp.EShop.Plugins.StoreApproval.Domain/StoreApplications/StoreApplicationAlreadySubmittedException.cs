using Volo.Abp;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplicationAlreadySubmittedException : BusinessException
    {
        public StoreApplicationAlreadySubmittedException()
            : base(StoreApprovalErrorCodes.AlreadySubmitted)
        {
            
        }
    }
}