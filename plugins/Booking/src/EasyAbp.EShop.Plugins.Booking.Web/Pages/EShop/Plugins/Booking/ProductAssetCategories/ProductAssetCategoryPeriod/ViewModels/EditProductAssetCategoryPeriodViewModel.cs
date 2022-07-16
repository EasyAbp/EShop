using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.
    ProductAssetCategoryPeriod.ViewModels
{
    public class EditProductAssetCategoryPeriodViewModel
    {
        [Display(Name = "ProductAssetCategoryPeriodCurrency")]
        [Required]
        public string Currency { get; set; }

        [Display(Name = "ProductAssetCategoryPeriodPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}