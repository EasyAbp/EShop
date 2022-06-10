using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.EShop.Products.Products;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class CreateEditProductViewModel : IValidatableObject
    {
        [HiddenInput]
        [Display(Name = "ProductStoreId")]
        public Guid StoreId { get; set; }
        
        [Required]
        [SelectItems("ProductGroups")]
        [Display(Name = "ProductProductGroupName")]
        public string ProductGroupName { get; set; }

        // [SelectItems("Categories")]
        [EasySelector(
            getListedDataSourceUrl: ProductsConsts.GetCategorySummaryListedDataSourceUrl,
            getSingleDataSourceUrl: ProductsConsts.GetCategorySummarySingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "displayName")]
        [Display(Name = "ProductCategory")]
        public List<Guid> CategoryIds { get; set; }

        [Display(Name = "ProductUniqueName")]
        public string UniqueName { get; set; }
        
        [Required]
        [Display(Name = "ProductDisplayName")]
        public string DisplayName { get; set; }

        public CreateEditProductDetailViewModel ProductDetail { get; set; } = new();

        [Required]
        [Placeholder("ProductAttributeNamesPlaceholder")]
        [Display(Name = "ProductAttributeNames")]
        public string ProductAttributeNames { get; set; }
        
        [Required]
        [TextArea(Rows = 4)]
        [Placeholder("ProductAttributeOptionNamesPlaceholder")]
        [Display(Name = "ProductAttributeOptionNames")]
        public string ProductAttributeOptionNames { get; set; }

        [Display(Name = "ProductInventoryStrategy")]
        public InventoryStrategy InventoryStrategy { get; set; }

        [Placeholder("ProductInventoryProviderNamePlaceholder")]
        [Display(Name = "ProductInventoryProviderName")]
        public string InventoryProviderName { get; set; }

        [Display(Name = "ProductDisplayOrder")]
        public int DisplayOrder { get; set; }

        [Display(Name = "ProductMediaResources")]
        public string MediaResources { get; set; }
        
        [Display(Name = "ProductIsPublished")]
        public bool IsPublished { get; set; }
        
        [Display(Name = "ProductIsHidden")]
        public bool IsHidden { get; set; }
        
        [Display(Name = "ProductPaymentExpireIn")]
        public TimeSpan? PaymentExpireIn { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StoreId != ProductDetail.StoreId)
            {
                yield return new ValidationResult(
                    "The StoreId should be same as the ProductDetail.StoreId.",
                    new[] {"StoreId"}
                );
            }
        }
    }
}