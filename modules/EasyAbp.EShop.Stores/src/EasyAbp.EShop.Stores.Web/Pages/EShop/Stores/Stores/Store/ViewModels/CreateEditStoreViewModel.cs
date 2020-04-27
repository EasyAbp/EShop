using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels
{
    public class CreateEditStoreViewModel
    {
        [Required]
        [Display(Name = "StoreName")]
        public string Name { get; set; }
    }
}