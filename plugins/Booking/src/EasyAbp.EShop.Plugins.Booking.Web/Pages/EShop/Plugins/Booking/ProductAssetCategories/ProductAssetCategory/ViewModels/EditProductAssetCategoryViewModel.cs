using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory.ViewModels
{
    public class EditProductAssetCategoryViewModel
    {
        [Display(Name = "ProductAssetCategoryFromTime")]
        public DateTime FromTime { get; set; }

        [Display(Name = "ProductAssetCategoryToTime")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "ProductAssetCategoryPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}