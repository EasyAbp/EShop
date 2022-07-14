using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class UpdateProductAssetDto
    {
        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        [Required]
        public string Currency { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}