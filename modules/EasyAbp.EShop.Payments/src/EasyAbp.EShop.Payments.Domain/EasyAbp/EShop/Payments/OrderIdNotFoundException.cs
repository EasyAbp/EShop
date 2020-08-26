using Volo.Abp;

namespace EasyAbp.EShop.Payments
{
    public class OrderIdNotFoundException : BusinessException
    {
        public OrderIdNotFoundException() : base(message: $"Cannot get valid OrderId from ExtraProperties.")
        {
        }
    }
}