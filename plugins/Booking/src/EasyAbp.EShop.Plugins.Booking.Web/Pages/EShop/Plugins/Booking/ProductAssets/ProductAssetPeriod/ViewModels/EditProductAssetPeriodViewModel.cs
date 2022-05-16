using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels
{
    public class EditProductAssetPeriodViewModel
    {
        [Display(Name = "ProductAssetPeriodPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}