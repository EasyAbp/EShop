using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category.ViewModels
{
    public class CreateEditCategoryViewModel
    {
        [Display(Name = "CategoryUniqueName")]
        public string UniqueName { get; set; }
        
        [Required]
        [Display(Name = "CategoryDisplayName")]
        public string DisplayName { get; set; }

        [Display(Name = "CategoryDescription")]
        public string Description { get; set; }

        [Display(Name = "CategoryMediaResources")]
        public string MediaResources { get; set; }
    }
}