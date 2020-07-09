using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;

namespace EasyAbp.EShop.Payments.Payments
{
    public interface IPayableCheckProvider
    {
        Task CheckAsync(CreatePaymentDto input, List<OrderDto> orders, Dictionary<string, object> paymentExtraProperties);
    }
}