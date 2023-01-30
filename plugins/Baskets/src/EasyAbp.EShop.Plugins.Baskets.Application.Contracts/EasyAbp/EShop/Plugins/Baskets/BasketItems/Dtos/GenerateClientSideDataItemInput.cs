using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class GenerateClientSideDataItemInput : ExtensibleObject
    {
        /// <summary>
        /// Reuse Id if set.
        /// </summary>
        public Guid? Id { get; set; }
        
        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }
        
        public int Quantity { get; set; }
        
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var result in base.Validate(validationContext))
            {
                yield return result;
            }
            
            if (Quantity <= 0)
            {
                yield return new ValidationResult(
                    "Quantity should be greater than 0.",
                    new[] { "Quantity" }
                );
            }
            
            if (BasketName.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    "BasketName should not be empty.",
                    new[] { "BasketName" }
                );
            }
        }
    }
}