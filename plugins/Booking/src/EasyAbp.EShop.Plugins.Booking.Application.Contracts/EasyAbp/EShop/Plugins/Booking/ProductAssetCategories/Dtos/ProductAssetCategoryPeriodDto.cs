using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos
{
    [Serializable]
    public class ProductAssetCategoryPeriodDto : EntityDto<Guid>
    {
        public Guid PeriodId { get; set; }

        public decimal Price { get; set; }
    }
}