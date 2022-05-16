using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos
{
    [Serializable]
    public class CreateProductAssetCategoryDto
    {
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public Guid AssetCategoryId { get; set; }

        public Guid PeriodSchemeId { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }
    }
}