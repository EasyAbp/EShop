using Volo.Abp;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplicationAlreadyApprovedException : BusinessException
    {
        public StoreApplicationAlreadyApprovedException()
            :base(StoreApprovalErrorCodes.AlreadyApproved)
        {
            
        }
    }
}