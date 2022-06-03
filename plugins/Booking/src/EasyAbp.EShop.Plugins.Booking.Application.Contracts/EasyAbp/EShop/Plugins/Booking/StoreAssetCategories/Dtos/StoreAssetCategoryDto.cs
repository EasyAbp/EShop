using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos
{
    [Serializable]
    public class StoreAssetCategoryDto : AuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid AssetCategoryId { get; set; }
    }
}