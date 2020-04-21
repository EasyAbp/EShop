using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class CreateUpdateProductDetailViewModel
    {
        [TextArea(Rows = 4)]
        [Display(Name = "ProductDetailDescription")]
        public string Description { get; set; }
    }
}