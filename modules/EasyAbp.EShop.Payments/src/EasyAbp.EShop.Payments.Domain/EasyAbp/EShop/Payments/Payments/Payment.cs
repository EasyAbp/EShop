using System;
using System.Collections.Generic;
using EasyAbp.PaymentService.Payments;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Payments.Payments
{
    public class Payment : FullAuditedAggregateRoot<Guid>, IPayment, IMultiTenant
    {
        #region Base properties

        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        [NotNull]
        public virtual string PaymentMethod { get; protected set; }

        [CanBeNull]
        public virtual string PayeeAccount { get; protected set; }

        [CanBeNull]
        public virtual string ExternalTradingCode { get; protected set; }

        [NotNull]
        public virtual string Currency { get; protected set; }

        public virtual decimal OriginalPaymentAmount { get; protected set; }

        public virtual decimal PaymentDiscount { get; protected set; }

        public virtual decimal ActualPaymentAmount { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        public virtual decimal PendingRefundAmount { get; protected set; }

        public virtual DateTime? CompletionTime { get; protected set; }

        public virtual DateTime? CanceledTime { get; protected set; }

        IEnumerable<IPaymentItem> IPayment.PaymentItems => PaymentItems;
        public virtual List<PaymentItem> PaymentItems { get; protected set; }

        #endregion

        protected Payment()
        {
        }

        public Payment(
            Guid id,
            Guid? tenantId,
            Guid userId,
            [NotNull] string paymentMethod,
            [CanBeNull] string payeeAccount,
            [CanBeNull] string externalTradingCode,
            [NotNull] string currency,
            decimal originalPaymentAmount,
            decimal paymentDiscount,
            decimal actualPaymentAmount,
            decimal refundAmount,
            decimal pendingRefundAmount,
            DateTime? completionTime,
            DateTime? canceledTime,
            DateTime creationTime) : base(id)
        {
            TenantId = tenantId;
            UserId = userId;
            CreationTime = creationTime;

            Update(userId, paymentMethod, payeeAccount, externalTradingCode, currency, originalPaymentAmount,
                paymentDiscount, actualPaymentAmount, refundAmount, pendingRefundAmount, completionTime, canceledTime);
        }

        public void Update(
            Guid userId,
            [NotNull] string paymentMethod,
            [CanBeNull] string payeeAccount,
            [CanBeNull] string externalTradingCode,
            [NotNull] string currency,
            decimal originalPaymentAmount,
            decimal paymentDiscount,
            decimal actualPaymentAmount,
            decimal refundAmount,
            decimal pendingRefundAmount,
            DateTime? completionTime,
            DateTime? canceledTime)
        {
            UserId = userId;
            PaymentMethod = paymentMethod;
            PayeeAccount = payeeAccount;
            ExternalTradingCode = externalTradingCode;
            Currency = currency;
            OriginalPaymentAmount = originalPaymentAmount;
            PaymentDiscount = paymentDiscount;
            ActualPaymentAmount = actualPaymentAmount;
            RefundAmount = refundAmount;
            PendingRefundAmount = pendingRefundAmount;
            CompletionTime = completionTime;
            CanceledTime = canceledTime;
        }

        public void SetPaymentItems(List<PaymentItem> paymentItems)
        {
            PaymentItems = paymentItems;
        }
    }
}