using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class CreateBasketItemDto : IValidatableObject
    {
        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? UserId { get; set; }
        
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }
        
        public int Quantity { get; set; }
        
        public new IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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