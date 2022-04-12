using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class EditProductSkuViewModel
    {
        [Display(Name = "ProductSkuName")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "ProductSkuCurrency")]
        public string Currency { get; set; }
        
        [Required]
        [Display(Name = "ProductSkuPrice")]
        [Range(UpdateProductSkuDto.MinimumPrice, UpdateProductSkuDto.MaximumPrice)]
        public decimal Price { get; set; }
     
        [Display(Name = "ProductSkuOriginalPrice")]
        [Range(UpdateProductSkuDto.MinimumPrice, UpdateProductSkuDto.MaximumPrice)]
        public decimal? OriginalPrice { get; set; }

        [Required]
        [DefaultValue(1)]
        [Display(Name = "ProductSkuOrderMinQuantity")]
        [Range(UpdateProductSkuDto.MinimumQuantity, UpdateProductSkuDto.MaximumQuantity)]
        public int OrderMinQuantity { get; set; }

        [Required]
        [DefaultValue(99)]
        [Display(Name = "ProductSkuOrderMaxQuantity")]
        [Range(UpdateProductSkuDto.MinimumQuantity, UpdateProductSkuDto.MaximumQuantity)]
        public int OrderMaxQuantity { get; set; }
        
        [DisplayName("ProductSkuPaymentExpireIn")]
        public TimeSpan? PaymentExpireIn { get; set; }
        
        [Display(Name = "ProductSkuMediaResources")]
        public string MediaResources { get; set; }

        public CreateEditSkuProductDetailViewModel ProductDetail { get; set; } = new();
    }
}