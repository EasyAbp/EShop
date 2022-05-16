using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos
{
    [Serializable]
    public class UpdateProductAssetCategoryPeriodDto
    {
        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal Price { get; set; }
    }
}