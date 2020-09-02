using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class CreatePaymentDto : ExtensibleObject
    {
        public string PaymentMethod { get; set; }

        public List<Guid> OrderIds { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);
            
            if (OrderIds.Count == 0)
            {
                yield return new ValidationResult(
                    "OrderIds is empty.",
                    new[] {"OrderIds"}
                );
            }

            if (OrderIds.Distinct().Count() != OrderIds.Count)
            {
                yield return new ValidationResult(
                    "OrderIds should be distinct.",
                    new[] {"OrderIds"}
                );
            }
        }
    }
}