using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Refunds;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class Refund : FullAuditedAggregateRoot<Guid>, IRefund, IMultiTenant
    {
        #region Base properties

        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid PaymentId { get; protected set; }

        [NotNull]
        public virtual string RefundPaymentMethod { get; protected set; }

        [NotNull]
        public virtual string ExternalTradingCode { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual string DisplayReason { get; protected set; }

        [CanBeNull]
        public virtual string CustomerRemark { get; protected set; }

        [CanBeNull]
        public virtual string StaffRemark { get; protected set; }

        public virtual DateTime? CompletedTime { get; protected set; }

        public virtual DateTime? CanceledTime { get; protected set; }

        #endregion

        IEnumerable<IRefundItem> IRefund.RefundItems => RefundItems;
        public virtual List<RefundItem> RefundItems { get; protected set; }

        protected Refund()
        {
        }

        public Refund(
            Guid id,
            Guid? tenantId,
            Guid paymentId,
            [NotNull] string refundPaymentMethod,
            [NotNull] string externalTradingCode,
            [NotNull] string currency,
            decimal refundAmount,
            string displayReason,
            [CanBeNull] string customerRemark,
            [CanBeNull] string staffRemark,
            DateTime? completedTime,
            DateTime? canceledTime) : base(id)
        {
            TenantId = tenantId;

            Update(paymentId, refundPaymentMethod, externalTradingCode, currency, refundAmount, displayReason,
                customerRemark, staffRemark, completedTime, canceledTime);
        }

        public void Update(
            Guid paymentId,
            [NotNull] string refundPaymentMethod,
            [NotNull] string externalTradingCode,
            [NotNull] string currency,
            decimal refundAmount,
            string displayReason,
            [CanBeNull] string customerRemark,
            [CanBeNull] string staffRemark,
            DateTime? completedTime,
            DateTime? canceledTime)
        {
            PaymentId = paymentId;
            RefundPaymentMethod = refundPaymentMethod;
            ExternalTradingCode = externalTradingCode;
            Currency = currency;
            RefundAmount = refundAmount;
            DisplayReason = displayReason;
            CustomerRemark = customerRemark;
            StaffRemark = staffRemark;
            CompletedTime = completedTime;
            CanceledTime = canceledTime;
        }

        public void SetRefundItems(List<RefundItem> refundItems)
        {
            RefundItems = refundItems;
        }
    }
}