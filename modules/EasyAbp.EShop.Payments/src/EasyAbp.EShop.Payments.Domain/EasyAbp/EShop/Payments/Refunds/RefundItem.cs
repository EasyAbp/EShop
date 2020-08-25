using System;
using EasyAbp.PaymentService.Refunds;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundItem : FullAuditedEntity<Guid>, IRefundItem
    {
        #region Base properties

        public virtual Guid PaymentItemId { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }
        
        public virtual string CustomerRemark { get; protected set; }
        
        public virtual string StaffRemark { get; protected set; }

        #endregion
        
        public virtual Guid? StoreId { get; protected set; }

        private RefundItem()
        {
            
        }
        
        public void SetStoreId(Guid? storeId)
        {
            StoreId = storeId;
        }
    }
}