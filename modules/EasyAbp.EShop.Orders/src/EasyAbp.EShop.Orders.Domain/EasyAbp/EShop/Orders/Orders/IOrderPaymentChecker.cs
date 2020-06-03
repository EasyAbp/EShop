using System.Threading.Tasks;
using EasyAbp.PaymentService.Payments;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderPaymentChecker
    {
        Task<bool> IsValidPaymentAsync(Order order, PaymentEto paymentEto);
    }
}