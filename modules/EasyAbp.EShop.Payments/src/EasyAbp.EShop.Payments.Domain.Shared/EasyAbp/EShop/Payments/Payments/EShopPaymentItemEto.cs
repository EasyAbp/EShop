using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Payments.Payments
{
    public class EShopPaymentItemEto : ExtensibleObject, IPaymentItem
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
        
        #endregion
        
        public Guid StoreId { get; set; }
    }
}