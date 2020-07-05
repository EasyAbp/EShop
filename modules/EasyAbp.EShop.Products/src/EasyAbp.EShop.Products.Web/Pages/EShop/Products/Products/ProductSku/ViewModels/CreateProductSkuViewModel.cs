using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class CreateProductSkuViewModel : EditProductSkuViewModel, IValidatableObject
    {
        [Required]
        [Display(Name = "ProductSkuInventory")]
        public int Inventory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Inventory < 0)
            {
                yield return new ValidationResult(
                    "Inventory should greater than or equal to 0.",
                    new[] { "Inventory" }
                );
            }
        }
    }
}