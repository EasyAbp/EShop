using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class CreateEditSkuProductDetailViewModel
    {
        [HiddenInput]
        public Guid? StoreId { get; set; }
        
        [TextArea(Rows = 4)]
        [Display(Name = "ProductDetailDescription")]
        [Placeholder("ProductSkuProductDetailDescriptionPlaceholder")]
        public string Description { get; set; }

        public bool HasContent()
        {
            return !Description.IsNullOrEmpty();
        }
    }
}