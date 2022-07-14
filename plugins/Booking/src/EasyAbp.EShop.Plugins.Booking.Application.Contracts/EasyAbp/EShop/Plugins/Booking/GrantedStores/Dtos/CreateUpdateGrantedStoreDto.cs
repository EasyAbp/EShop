using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos
{
    [Serializable]
    public class CreateUpdateGrantedStoreDto
    {
        public Guid StoreId { get; set; }

        public Guid? AssetId { get; set; }

        public Guid? AssetCategoryId { get; set; }

        public bool AllowAll { get; set; }
    }
}