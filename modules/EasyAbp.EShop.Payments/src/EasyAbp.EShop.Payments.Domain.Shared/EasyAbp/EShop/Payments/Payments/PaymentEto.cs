using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class PaymentEto
    {
        public Guid Id { get; set; }
        
        public Guid? TenantId { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public string ExternalTradingCode { get; set; }
        
        public string Currency { get; set; }
        
        public decimal OriginalPaymentAmount { get; set; }

        public decimal PaymentDiscount { get; set; }
        
        public decimal ActualPaymentAmount { get; set; }
        
        public decimal RefundAmount { get; set; }
        
        public DateTime? CompletionTime { get; set; }
        
        public List<PaymentItemEto> PaymentItems { get; set; }
    }
}