using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos
{
    [Serializable]
    public class UpdateProductAssetCategoryDto
    {
        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        [Required]
        public string Currency { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}