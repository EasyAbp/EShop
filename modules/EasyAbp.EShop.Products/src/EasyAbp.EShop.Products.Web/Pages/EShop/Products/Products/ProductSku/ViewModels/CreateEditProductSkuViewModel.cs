using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class CreateEditProductSkuViewModel
    {
        [Display(Name = "ProductSkuCode")]
        public string Code { get; set; }
        
        [Required]
        [Display(Name = "ProductSkuCurrency")]
        public string Currency { get; set; }
        
        [Required]
        [Display(Name = "ProductSkuPrice")]
        public decimal Price { get; set; }
     
        [Display(Name = "ProductSkuOriginalPrice")]
        public decimal? OriginalPrice { get; set; }

        [Required]
        [Display(Name = "ProductSkuInventory")]
        public int Inventory { get; set; }

        [Required]
        [DefaultValue(1)]
        [Display(Name = "ProductSkuOrderMinQuantity")]
        public int OrderMinQuantity { get; set; }

        [Required]
        [DefaultValue(99)]
        [Display(Name = "ProductSkuOrderMaxQuantity")]
        public int OrderMaxQuantity { get; set; }
        
        [Display(Name = "ProductSkuMediaResources")]
        public string MediaResources { get; set; }
        
        [HiddenInput]
        [Display(Name = "ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }
    }
}