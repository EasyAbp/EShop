using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class ProductAssetPeriodDto : EntityDto<Guid>
    {
        public Guid PeriodId { get; set; }

        public decimal Price { get; set; }
    }
}