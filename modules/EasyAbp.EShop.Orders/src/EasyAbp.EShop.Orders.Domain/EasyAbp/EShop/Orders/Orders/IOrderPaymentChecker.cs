using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Payments;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IEShopPaymentChecker
    {
        Task<bool> IsValidPaymentAsync(Order order, EShopPaymentEto payment, EShopPaymentItemEto paymentItem);
    }
}