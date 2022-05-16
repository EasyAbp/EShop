using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class UpdateProductAssetPeriodDto
    {
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}