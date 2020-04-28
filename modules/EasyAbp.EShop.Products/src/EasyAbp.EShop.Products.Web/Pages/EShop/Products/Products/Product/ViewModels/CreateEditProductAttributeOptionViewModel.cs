using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class CreateEditProductAttributeOptionViewModel
    {
        [Required]
        [Display(Name = "ProductAttributeOptionDisplayName")]
        public string DisplayName { get; set; }
        
        [Display(Name = "ProductAttributeOptionDescription")]
        public string Description { get; set; }

        [Display(Name = "ProductAttributeOptionDisplayOrder")]
        public int DisplayOrder { get; set; } = 0;
    }
}