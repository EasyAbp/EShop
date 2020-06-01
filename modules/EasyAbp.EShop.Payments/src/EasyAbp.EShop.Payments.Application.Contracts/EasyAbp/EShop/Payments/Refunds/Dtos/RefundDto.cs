using System;
using Volo.Abp.Application.Dtos;
using PaymentServiceRefundDto = EasyAbp.PaymentService.Refunds.Dtos.RefundDto;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class RefundDto : PaymentServiceRefundDto
    {
        public Guid? StoreId { get; set; }
    }
}