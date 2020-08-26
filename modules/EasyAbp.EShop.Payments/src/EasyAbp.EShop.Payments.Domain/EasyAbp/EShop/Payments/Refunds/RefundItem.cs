using System;
using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundItem : FullAuditedEntity<Guid>, IRefundItem, IMultiStore, IHasExtraProperties
    {
        #region Base properties

        public virtual Guid PaymentItemId { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual string CustomerRemark { get; protected set; }
        
        public virtual string StaffRemark { get; protected set; }

        public virtual Dictionary<string, object> ExtraProperties { get; set; }

        #endregion
        
        public virtual Guid StoreId { get; protected set; }
        
        public virtual Guid OrderId { get; protected set; }
        
        public virtual List<RefundItemOrderLine> RefundItemOrderLines { get; protected set; }

        protected RefundItem()
        {
            ExtraProperties = new Dictionary<string, object>();
            RefundItemOrderLines = new List<RefundItemOrderLine>();
        }
        
        public void SetStoreId(Guid storeId)
        {
            StoreId = storeId;
        }
        
        public void SetOrderId(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}