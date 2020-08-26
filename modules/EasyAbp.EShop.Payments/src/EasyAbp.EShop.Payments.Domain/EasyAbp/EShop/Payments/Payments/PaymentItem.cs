using System;
using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.PaymentService.Payments;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentItem : FullAuditedEntity<Guid>, IPaymentItem, IHasExtraProperties, IMultiStore
    {
        #region Base properties

        [NotNull]
        public virtual string ItemType { get; protected set; }
        
        public virtual string ItemKey { get; protected set; }

        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }
        
        public virtual decimal ActualPaymentAmount { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual decimal PendingRefundAmount { get; protected set; }
        
        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        #endregion
        
        public virtual Guid StoreId { get; protected set; }

        public void SetStoreId(Guid storeId)
        {
            StoreId = storeId;
        }
        
        private PaymentItem()
        {
            ExtraProperties = new Dictionary<string, object>();
        }
    }
}
