using System.Collections.Generic;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.PaymentService.Payments;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentCreationResource
    {
        public CreatePaymentDto Input { get; set; }
        
        public List<OrderDto> Orders { get; set; }
    }
}