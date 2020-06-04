using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class CreatePaymentDto : IValidatableObject
    {
        public string PaymentMethod { get; set; }

        public List<Guid> OrderIds { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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