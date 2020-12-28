using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Payments.Payments
{
    [Serializable]
    public class EShopPaymentItemEto : IPaymentItem
    {
        #region Base properties

        public Guid Id { get; set; }
        
        public string ItemType { get; set; }

        public string ItemKey { get; set; }

        public decimal OriginalPaymentAmount { get; set; }

        public decimal PaymentDiscount { get; set; }

        public decimal ActualPaymentAmount { get; set; }

        public decimal RefundAmount { get; set; }
        
        public decimal PendingRefundAmount { get; set; }
        
        public ExtraPropertyDictionary ExtraProperties { get; set; }
        
        #endregion
        
        public Guid StoreId { get; set; }
    }
}