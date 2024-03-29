using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels
{
    public class CreateProductAssetViewModel
    {
        [HiddenInput]
        [Display(Name = "ProductAssetStoreId")]
        public Guid StoreId { get; set; }

        [Display(Name = "ProductAssetProductId")]
        public Guid ProductId { get; set; }

        [Display(Name = "ProductAssetProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [Display(Name = "ProductAssetAssetId")]
        public Guid AssetId { get; set; }

        [Display(Name = "ProductAssetPeriodSchemeId")]
        public Guid PeriodSchemeId { get; set; }

        [Display(Name = "ProductAssetFromTime")]
        public DateTime FromTime { get; set; }

        [Display(Name = "ProductAssetToTime")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "ProductAssetCurrency")]
        public string Currency { get; set; }

        [Display(Name = "ProductAssetPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}