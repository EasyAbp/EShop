using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class UpdateBasketItemDto : IValidatableObject
    {
        public int Quantity { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity <= 0)
            {
                yield return new ValidationResult(
                    "Quantity should be greater than 0.",
                    new[] { "Quantity" }
                );
            }
        }
    }
}