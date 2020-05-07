using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentPayeeAccountProvider
    {
        Task<string> GetPayeeAccountAsync(Payment payment, Dictionary<string, object> inputExtraProperties);
    }
}