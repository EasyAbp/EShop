using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class CreateEditProductViewModel
    {
        [HiddenInput]
        [Display(Name = "ProductStore")]
        public Guid StoreId { get; set; }

        [Required]
        [SelectItems("ProductTypes")]
        [Display(Name = "ProductType")]
        public Guid ProductTypeId { get; set; }

        [SelectItems("Categories")]
        [Display(Name = "ProductCategory")]
        public List<Guid> CategoryIds { get; set; }
        
        [Required]
        [Display(Name = "ProductDisplayName")]
        public string DisplayName { get; set; }
        
        public CreateEditProductDetailViewModel ProductDetail { get; set; }

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
        
        [Display(Name = "ProductDisplayOrder")]
        public int DisplayOrder { get; set; } = 0;

        [Display(Name = "ProductMediaResources")]
        public string MediaResources { get; set; }
        
        [Display(Name = "ProductIsPublished")]
        public bool IsPublished { get; set; }
    }
}