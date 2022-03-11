using Volo.Abp;

namespace EasyAbp.EShop.Payments
{
    public class StoreIdNotFoundException : BusinessException
    {
        public StoreIdNotFoundException() : base(PaymentsErrorCodes.StoreIdNotFound)
        {
        }
    }
}