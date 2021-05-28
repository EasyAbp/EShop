using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.Instocks.Dtos
{
    [Serializable]
    public class InstockUpdateDto : IMultiStore
    {
        [DisplayName("InstockInstockTime")]
        public DateTime InstockTime { get; set; }

        [DisplayName("InstockProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("InstockDescription")]
        public string Description { get; set; }

        [DisplayName("InstockUnitPrice")]
        public decimal UnitPrice { get; set; }

        [DisplayName("InstockUnits")]
        public int Units { get; set; }

        [DisplayName("InstockOperatorName")]
        public string OperatorName { get; set; }

        [DisplayName("InstockWarehouseId")]
        public Guid WarehouseId { get; set; }

        [DisplayName("InstockStoreId")]
        public Guid StoreId { get; set; }

    }
}