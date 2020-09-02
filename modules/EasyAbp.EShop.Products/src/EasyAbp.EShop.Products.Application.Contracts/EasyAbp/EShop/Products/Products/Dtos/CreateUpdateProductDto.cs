using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductDto : ExtensibleObject
    {
        [DisplayName("ProductProductGroupName")]
        public string ProductGroupName { get; set; }
        
        [DisplayName("ProductDetailId")]
        public Guid ProductDetailId { get; set; }
        
        /// <summary>
        /// This property is set for adding the store to ProductStore in creation, or for permission checking in update.
        /// </summary>
        [DisplayName("ProductStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("ProductCategory")]
        public ICollection<Guid> CategoryIds { get; set; }
        
        [DisplayName("ProductUniqueName")]
        public string UniqueName { get; set; }
        
        [Required]
        [DisplayName("ProductDisplayName")]
        public string DisplayName { get; set; }
        
        public ICollection<CreateUpdateProductAttributeDto> ProductAttributes { get; set; }

        [DisplayName("ProductInventoryStrategy")]
        public InventoryStrategy InventoryStrategy { get; set; }
        
        [DisplayName("ProductDisplayOrder")]
        public int DisplayOrder { get; set; }

        [DisplayName("ProductMediaResources")]
        public string MediaResources { get; set; }

        [DisplayName("ProductIsPublished")]
        public bool IsPublished { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            base.Validate(validationContext);
            
            if (ProductAttributes.Select(a => a.DisplayName.Trim()).Distinct().Count() != ProductAttributes.Count)
            {
                yield return new ValidationResult(
                    "DisplayNames of ProductAttributes should be unique!",
                    new[] { "ProductAttributes" }
                );
            }

            var optionNameList = ProductAttributes.SelectMany(a => a.ProductAttributeOptions)
                .Select(o => o.DisplayName.Trim()).ToList();
            
            if (optionNameList.Distinct().Count() != optionNameList.Count)
            {
                yield return new ValidationResult(
                    "DisplayNames of ProductAttributeOptions should be unique!",
                    new[] { "ProductAttributeOptions" }
                );
            }
        }
    }
}