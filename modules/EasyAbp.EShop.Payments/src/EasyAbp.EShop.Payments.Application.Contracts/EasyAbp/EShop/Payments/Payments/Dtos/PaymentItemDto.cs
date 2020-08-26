using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class PaymentItemDto : ExtensibleFullAuditedEntityDto<Guid>, IPaymentItem
    {
        public Guid StoreId { get; set; }

        public string ItemType { get; set; }

        public string ItemKey { get; set; }

        public string Currency { get; set; }

        public decimal OriginalPaymentAmount { get; set; }

        public decimal PaymentDiscount { get; set; }

        public decimal ActualPaymentAmount { get; set; }

        public decimal RefundAmount { get; set; }
        
        public decimal PendingRefundAmount { get; set; }
    }
}