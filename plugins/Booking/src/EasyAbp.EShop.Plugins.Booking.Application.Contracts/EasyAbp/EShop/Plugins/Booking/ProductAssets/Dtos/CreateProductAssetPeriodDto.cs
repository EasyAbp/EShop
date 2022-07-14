using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class CreateProductAssetPeriodDto
    {
        public Guid PeriodId { get; set; }

        [Required]
        public string Currency { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}