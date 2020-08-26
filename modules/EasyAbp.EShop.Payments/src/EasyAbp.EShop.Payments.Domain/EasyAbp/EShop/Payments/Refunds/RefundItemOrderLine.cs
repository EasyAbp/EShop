using System;
using AutoMapper;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Payments.Refunds
{
    [AutoMap(typeof(RefundItemOrderLineEto))]
    public class RefundItemOrderLine : Entity<Guid>
    {
        public virtual Guid OrderLineId { get; protected set; }
        
        public virtual int RefundedQuantity { get; protected set; }
        
        public virtual decimal RefundAmount { get; protected set; }

        protected RefundItemOrderLine()
        {
        }

        public RefundItemOrderLine(
            Guid id,
            Guid orderLineId,
            int refundedQuantity,
            decimal refundAmount
        ) : base(id)
        {
            OrderLineId = orderLineId;
            RefundedQuantity = refundedQuantity;
            RefundAmount = refundAmount;
        }
    }
}