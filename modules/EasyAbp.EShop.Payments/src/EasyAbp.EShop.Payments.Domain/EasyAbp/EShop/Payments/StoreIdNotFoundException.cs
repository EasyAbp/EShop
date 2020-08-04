using Volo.Abp;

namespace EasyAbp.EShop.Payments
{
    public class StoreIdNotFoundException : BusinessException
    {
        public StoreIdNotFoundException() : base(message: $"Cannot get valid StoreId from ExtraProperties.")
        {
        }
    }
}