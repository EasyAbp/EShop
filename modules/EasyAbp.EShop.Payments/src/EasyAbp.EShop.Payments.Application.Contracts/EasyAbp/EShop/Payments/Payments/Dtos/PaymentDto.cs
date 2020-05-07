using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class PaymentDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        public string PaymentMethod { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Currency { get; set; }

        public decimal OriginalPaymentAmount { get; set; }

        public decimal PaymentDiscount { get; set; }

        public decimal ActualPaymentAmount { get; set; }

        public decimal RefundAmount { get; set; }

        public DateTime? CompletionTime { get; set; }

        public List<PaymentItemDto> PaymentItems { get; set; }
    }
}