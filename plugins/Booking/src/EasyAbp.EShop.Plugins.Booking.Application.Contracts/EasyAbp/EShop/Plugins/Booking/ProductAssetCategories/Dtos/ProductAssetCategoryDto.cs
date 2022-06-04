using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos
{
    [Serializable]
    public class ProductAssetCategoryDto : AuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public Guid AssetCategoryId { get; set; }

        public Guid PeriodSchemeId { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        public decimal? Price { get; set; }

        public List<ProductAssetCategoryPeriodDto> Periods { get; set; }
    }
}