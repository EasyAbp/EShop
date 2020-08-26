using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class CreateEShopRefundInput : IValidatableObject
    {
        public Guid PaymentId { get; set; }
        
        [CanBeNull]
        public string DisplayReason { get; set; }

        [CanBeNull]
        public string CustomerRemark { get; set; }
        
        [CanBeNull]
        public string StaffRemark { get; set; }
        
        public List<CreateEShopRefundItemInput> RefundItems { get; set; } = new List<CreateEShopRefundItemInput>();
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (RefundItems.IsNullOrEmpty())
            {
                yield return new ValidationResult(
                    "RefundItems should not be empty!",
                    new[] { nameof(RefundItems) }
                );
            }
            
            if (RefundItems.Any(x => x.OrderLines.IsNullOrEmpty()))
            {
                yield return new ValidationResult(
                    "RefundItem.OrderLines should not be empty!",
                    new[] { nameof(CreateEShopRefundItemInput.OrderLines) }
                );
            }
        }
    }
}