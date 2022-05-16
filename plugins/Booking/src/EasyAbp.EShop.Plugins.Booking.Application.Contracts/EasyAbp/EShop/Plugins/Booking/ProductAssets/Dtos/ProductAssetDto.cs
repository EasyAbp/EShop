using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class ProductAssetDto : AuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public Guid AssetId { get; set; }

        public Guid PeriodSchemeId { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        public decimal? Price { get; set; }

        public List<ProductAssetPeriodDto> Periods { get; set; }
    }
}