using System;
using System.Collections.Generic;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.DynamicProxy;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundItem : FullAuditedEntity<Guid>, IRefundItem, IMultiStore
    {
        #region Base properties

        public virtual Guid PaymentItemId { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual string CustomerRemark { get; protected set; }

        public virtual string StaffRemark { get; protected set; }

        public virtual ExtraPropertyDictionary ExtraProperties { get; set; }

        #endregion

        public virtual Guid StoreId { get; protected set; }

        public virtual Guid OrderId { get; protected set; }

        public virtual List<RefundItemOrderLine> OrderLines { get; protected set; }

        public virtual List<RefundItemOrderExtraFee> OrderExtraFees { get; protected set; }

        protected RefundItem()
        {
            OrderLines = new List<RefundItemOrderLine>();
            OrderExtraFees = new List<RefundItemOrderExtraFee>();

            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
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