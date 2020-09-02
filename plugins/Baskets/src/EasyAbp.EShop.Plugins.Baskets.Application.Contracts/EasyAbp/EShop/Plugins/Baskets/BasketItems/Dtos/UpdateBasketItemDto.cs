using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class UpdateBasketItemDto : ExtensibleObject
    {
        public int Quantity { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);
            
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