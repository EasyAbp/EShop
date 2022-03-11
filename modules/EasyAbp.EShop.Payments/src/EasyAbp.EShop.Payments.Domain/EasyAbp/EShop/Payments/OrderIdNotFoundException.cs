using Volo.Abp;

namespace EasyAbp.EShop.Payments
{
    public class OrderIdNotFoundException : BusinessException
    {
        public OrderIdNotFoundException() : base(PaymentsErrorCodes.OrderIdNotFound)
        {
        }
    }
}