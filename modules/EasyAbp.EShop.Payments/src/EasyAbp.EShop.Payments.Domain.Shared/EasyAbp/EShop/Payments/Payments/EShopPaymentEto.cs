using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class EShopPaymentEto : IPayment, IMultiTenant
    {
        #region Base properties

        public Guid Id { get; set; }

        public Guid? TenantId { get; set; }
        
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

        public Dictionary<string, object> ExtraProperties { get; set; }
        
        #endregion

        public List<EShopPaymentItemEto> PaymentItems { get; set; } = new List<EShopPaymentItemEto>();
    }
}