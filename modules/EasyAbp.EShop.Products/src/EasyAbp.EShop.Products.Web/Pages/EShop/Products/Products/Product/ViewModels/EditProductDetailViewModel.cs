using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class EditProductDetailViewModel
    {
        [TextArea(Rows = 4)]
        [Display(Name = "ProductDetailDescription")]
        public string Description { get; set; }
    }
}