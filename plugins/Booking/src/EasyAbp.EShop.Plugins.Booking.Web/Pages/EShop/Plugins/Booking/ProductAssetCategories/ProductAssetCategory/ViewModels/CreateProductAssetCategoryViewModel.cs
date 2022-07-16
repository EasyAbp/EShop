using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory.ViewModels
{
    public class CreateProductAssetCategoryViewModel
    {
        [HiddenInput]
        [Display(Name = "ProductAssetCategoryStoreId")]
        public Guid StoreId { get; set; }

        [Display(Name = "ProductAssetCategoryProductId")]
        public Guid ProductId { get; set; }

        [Display(Name = "ProductAssetCategoryProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [Display(Name = "ProductAssetCategoryAssetCategoryId")]
        public Guid AssetCategoryId { get; set; }

        [Display(Name = "ProductAssetCategoryPeriodSchemeId")]
        public Guid PeriodSchemeId { get; set; }

        [Display(Name = "ProductAssetCategoryFromTime")]
        public DateTime FromTime { get; set; }

        [Display(Name = "ProductAssetCategoryToTime")]
        public DateTime? ToTime { get; set; }

        [Display(Name = "ProductAssetCategoryCurrency")]
        public string Currency { get; set; }

        [Display(Name = "ProductAssetCategoryPrice")]
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}