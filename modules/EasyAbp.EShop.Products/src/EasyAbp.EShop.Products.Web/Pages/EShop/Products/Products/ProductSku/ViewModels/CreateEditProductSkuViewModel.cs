using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class CreateEditProductSkuViewModel
    {
        [Required]
        [Display(Name = "ProductSkuCurrency")]
        public string Currency { get; set; }
        
        [Required]
        [Display(Name = "ProductSkuPrice")]
        public decimal Price { get; set; }
     
        [Display(Name = "ProductSkuOriginalPrice")]
        public decimal OriginalPrice { get; set; }

        [Required]
        [Display(Name = "ProductSkuInventory")]
        public int Inventory { get; set; }

        [Required]
        [Display(Name = "ProductSkuOrderMinQuantity")]
        public int OrderMinQuantity { get; set; }
        
        [HiddenInput]
        [Display(Name = "ProductSkuProductDetailId")]
        public Guid? ProductDetailId { get; set; }
    }
}