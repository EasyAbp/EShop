using System;
using AutoMapper;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Payments.Refunds
{
    [AutoMap(typeof(RefundItemOrderExtraFeeEto))]
    public class RefundItemOrderExtraFee : Entity<Guid>
    {
        [NotNull]
        public virtual string Name { get; protected set; }

        [CanBeNull]
        public virtual string Key { get; protected set; }

        [CanBeNull]
        public virtual string DisplayName { get; protected set; }

        public virtual decimal RefundAmount { get; protected set; }

        protected RefundItemOrderExtraFee()
        {
        }

        public RefundItemOrderExtraFee(
            Guid id,
            [NotNull] string name,
            [CanBeNull] string key,
            [CanBeNull] string displayName,
            decimal refundAmount) : base(id)
        {
            Name = name;
            Key = key;
            DisplayName = displayName;
            RefundAmount = refundAmount;
        }
    }
}