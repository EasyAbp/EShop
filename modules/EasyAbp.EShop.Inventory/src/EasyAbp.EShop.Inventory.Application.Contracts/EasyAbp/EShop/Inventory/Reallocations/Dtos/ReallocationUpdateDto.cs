using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Inventory.Reallocations.Dtos
{
    [Serializable]
    public class ReallocationUpdateDto : IMultiStore
    {
        [DisplayName("ReallocationProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("ReallocationSourceWarehouseId")]
        public Guid SourceWarehouseId { get; set; }

        [DisplayName("ReallocationDestinationWarehouseId")]
        public Guid DestinationWarehouseId { get; set; }

        [DisplayName("ReallocationUnits")]
        public int Units { get; set; }

        [DisplayName("ReallocationOperatorName")]
        public string OperatorName { get; set; }

        [DisplayName("ReallocationReallocationTime")]
        public DateTime ReallocationTime { get; set; }

        [DisplayName("ReallocationDescription")]
        public string Description { get; set; }

        [DisplayName("ReallocationStoreId")]
        public Guid StoreId { get; set; }

    }
}