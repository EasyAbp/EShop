using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels
{
    public class EditProductAssetViewModel
    {
        [Display(Name = "ProductAssetFromTime")]
        public DateTime FromTime { get; set; }

        [Display(Name = "ProductAssetToTime")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "ProductAssetPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}