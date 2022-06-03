using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos
{
    [Serializable]
    public class CreateUpdateStoreAssetCategoryDto
    {
        public Guid StoreId { get; set; }

        public Guid AssetCategoryId { get; set; }
    }
}