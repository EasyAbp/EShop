using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels
{
    public class CreateProductAssetPeriodViewModel
    {
        [Display(Name = "ProductAssetPeriodPeriodId")]
        public Guid PeriodId { get; set; }

        [Display(Name = "ProductAssetPeriodCurrency")]
        [Required]
        public string Currency { get; set; }

        [Display(Name = "ProductAssetPeriodPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}