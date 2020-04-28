using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class CreateEditProductAttributeViewModel
    {
        [Required]
        [Display(Name = "ProductAttributeDisplayName")]
        public string DisplayName { get; set; }
        
        [Display(Name = "ProductAttributeDescription")]
        public string Description { get; set; }
        
        [Display(Name = "ProductAttributeDisplayOrder")]
        public int DisplayOrder { get; set; } = 0;
        
        public List<CreateEditProductAttributeOptionViewModel> ProductAttributeOptions { get; set; }
    }
}