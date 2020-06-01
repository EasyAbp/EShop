using System;
using PaymentServicePaymentDto = EasyAbp.PaymentService.Payments.Dtos.PaymentDto;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class PaymentDto : PaymentServicePaymentDto
    {
        public Guid? StoreId { get; set; }
    }
}