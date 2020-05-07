using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentAuthorizer
    {
        Task<bool> IsPaymentItemAllowedAsync(Payment payment, PaymentItem paymentItem,
            Dictionary<string, object> inputExtraProperties);
    }
}