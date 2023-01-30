using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class UpdateProductSkuDto : ExtensibleObject
    {
        public const double MinimumPrice = 0f;
        public const double MaximumPrice = 999999999999.99999999f;
        public const int MinimumQuantity = 1;
        public const int MaximumQuantity = int.MaxValue;
        
        [DisplayName("ProductSkuName")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("ProductSkuCurrency")]
        public string Currency { get; set; }
        
        [DisplayName("ProductSkuOriginalPrice")]
        [Range(MinimumPrice, MaximumPrice)]
        public decimal? OriginalPrice { get; set; }

        [DisplayName("ProductSkuPrice")]
        [Range(MinimumPrice, MaximumPrice)]
        public decimal Price { get; set; }
        
        [DefaultValue(1)]
        [DisplayName("ProductSkuOrderMinQuantity")]
        [Range(MinimumQuantity, MaximumQuantity)]
        public int OrderMinQuantity { get; set; }
        
        [DefaultValue(99)]
        [DisplayName("ProductSkuOrderMaxQuantity")]
        [Range(MinimumQuantity, MaximumQuantity)]
        public int OrderMaxQuantity { get; set; }
        
        [DisplayName("ProductSkuPaymentExpireIn")]
        public TimeSpan? PaymentExpireIn { get; set; }

        [DisplayName("ProductSkuMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var result in base.Validate(validationContext))
            {
                yield return result;
            }
            
            if (PaymentExpireIn.HasValue && PaymentExpireIn.Value < TimeSpan.Zero)
            {
                yield return new ValidationResult(
                    "PaymentExpireIn should be greater than or equal to 0.",
                    new[] { "PaymentExpireIn" }
                );
            }
        }
    }
}