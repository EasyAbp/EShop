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
    public class CreateEditProductDetailViewModel
    {
        [HiddenInput]
        [Display(Name = "ProductStore")]
        public Guid StoreId { get; set; }
        
        [TextArea(Rows = 4)]
        [Display(Name = "ProductDetailDescription")]
        public string Description { get; set; }
    }
}