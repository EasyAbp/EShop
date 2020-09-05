using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    [Serializable]
    public class PaymentDto : ExtensibleFullAuditedEntityDto<Guid>, IPayment
    {
        #region Base properties

        public Guid UserId { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public string PayeeAccount { get; set; }

        public string ExternalTradingCode { get; set; }

        public string Currency { get; set; }

        public decimal OriginalPaymentAmount { get; set; }

        public decimal PaymentDiscount { get; set; }

        public decimal ActualPaymentAmount { get; set; }

        public decimal RefundAmount { get; set; }
        
        public decimal PendingRefundAmount { get; set; }

        public DateTime? CompletionTime { get; set; }
        
        public DateTime? CanceledTime { get; set; }
        
        #endregion
        
        public List<PaymentItemDto> PaymentItems { get; set; }
    }
}