using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class CreateProductAssetDto
    {
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public Guid AssetId { get; set; }

        public Guid PeriodSchemeId { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}