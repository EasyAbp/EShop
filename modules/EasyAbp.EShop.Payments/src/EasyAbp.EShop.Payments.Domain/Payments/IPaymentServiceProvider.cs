using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPaymentServiceProvider
    {
        Task<Payment> PayForOrderAsync(Payment payment, Dictionary<string, object> extraProperties = null);
    }
}