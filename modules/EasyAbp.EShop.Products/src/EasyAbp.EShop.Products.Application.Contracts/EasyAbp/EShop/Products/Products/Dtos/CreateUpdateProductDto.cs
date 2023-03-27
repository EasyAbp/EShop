using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class CreateUpdateProductDto : ExtensibleObject, IMultiStore
    {
        [DisplayName("ProductStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("ProductProductGroupName")]
        public string ProductGroupName { get; set; }

        [DisplayName("ProductDetailId")]
        public Guid? ProductDetailId { get; set; }

        [DisplayName("ProductCategory")]
        public ICollection<Guid> CategoryIds { get; set; }

        [DisplayName("ProductUniqueName")]
        public string UniqueName { get; set; }

        [Required]
        [DisplayName("ProductDisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("ProductOverview")]
        public string Overview { get; set; }

        public ICollection<CreateUpdateProductAttributeDto> ProductAttributes { get; set; }

        [DisplayName("ProductInventoryStrategy")]
        public InventoryStrategy InventoryStrategy { get; set; }

        [DisplayName("ProductInventoryProviderName")]
        public string InventoryProviderName { get; set; }

        [DisplayName("ProductDisplayOrder")]
        public int DisplayOrder { get; set; }

        [DisplayName("ProductMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("ProductIsPublished")]
        public bool IsPublished { get; set; }

        [DisplayName("ProductIsHidden")]
        public bool IsHidden { get; set; }

        [DisplayName("ProductPaymentExpireIn")]
        public TimeSpan? PaymentExpireIn { get; set; }

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

            if (ProductAttributes.Select(a => a.DisplayName.Trim()).Distinct().Count() != ProductAttributes.Count)
            {
                yield return new ValidationResult(
                    "DisplayNames of ProductAttributes should be unique!",
                    new[] { "ProductAttributes" }
                );
            }

            foreach (var productAttribute in ProductAttributes)
            {
                var options = productAttribute.ProductAttributeOptions;

                if (options.Select(o => o.DisplayName.Trim()).Distinct().Count() != options.Count)
                {
                    yield return new ValidationResult(
                        "DisplayNames of ProductAttributeOptions should be unique!",
                        new[] { "ProductAttributeOptions" }
                    );
                }
            }
        }
    }
}