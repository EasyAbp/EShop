using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class CreateEShopRefundInput : ExtensibleObject
    {
        public Guid PaymentId { get; set; }
        
        [CanBeNull]
        public string DisplayReason { get; set; }

        [CanBeNull]
        public string CustomerRemark { get; set; }
        
        [CanBeNull]
        public string StaffRemark { get; set; }
        
        public List<CreateEShopRefundItemInput> RefundItems { get; set; } = new();
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var result in base.Validate(validationContext))
            {
                yield return result;
            }
            
            if (RefundItems.IsNullOrEmpty())
            {
                yield return new ValidationResult(
                    "RefundItems should not be empty!",
                    new[] { nameof(RefundItems) }
                );
            }
            
            if (RefundItems.Any(x => x.OrderLines.IsNullOrEmpty() && x.OrderExtraFees.IsNullOrEmpty()))
            {
                yield return new ValidationResult(
                    "RefundItem.OrderLines and RefundItem.OrderExtraFees should not both be empty!",
                    new[]
                    {
                        nameof(CreateEShopRefundItemInput.OrderLines), nameof(CreateEShopRefundItemInput.OrderExtraFees)
                    }
                );
            }

            if (RefundItems.SelectMany(x => x.OrderLines).Any(x => x.TotalAmount <= decimal.Zero))
            {
                yield return new ValidationResult(
                    "RefundAmount should be greater than 0.",
                    new[]
                    {
                        nameof(OrderLineRefundInfoModel.TotalAmount)
                    }
                );
            }

            if (RefundItems.SelectMany(x => x.OrderExtraFees).Any(x => x.TotalAmount <= decimal.Zero))
            {
                yield return new ValidationResult(
                    "RefundAmount should be greater than 0.",
                    new[]
                    {
                        nameof(OrderExtraFeeRefundInfoModel.TotalAmount)
                    }
                );
            }
        }
    }
}