using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public interface IPaymentOpenIdProvider
    {
        Task<string> FindUserOpenIdAsync(Guid userId);
    }
}