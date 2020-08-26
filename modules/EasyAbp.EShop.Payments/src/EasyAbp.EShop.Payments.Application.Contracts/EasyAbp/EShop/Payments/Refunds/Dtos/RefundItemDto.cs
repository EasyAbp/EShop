using System;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class RefundItemDto : ExtensibleFullAuditedEntityDto<Guid>, IRefund
    {
        public virtual Guid PaymentId { get; set; }

        public virtual string RefundPaymentMethod { get; set; }

        public virtual string ExternalTradingCode { get; set; }

        public virtual string Currency { get; set; }

        public virtual decimal RefundAmount { get; set; }

        public virtual string DisplayReason { get; set; }

        public virtual string CustomerRemark { get; set; }

        public virtual string StaffRemark { get; set; }

        public virtual DateTime? CompletedTime { get; set; }

        public virtual DateTime? CanceledTime { get; set; }

    }
}