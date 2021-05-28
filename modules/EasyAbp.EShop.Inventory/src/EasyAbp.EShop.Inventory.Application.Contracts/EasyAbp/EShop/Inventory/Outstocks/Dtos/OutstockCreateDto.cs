using EasyAbp.EShop.Stores.Stores;
using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Inventory.Outstocks.Dtos
{
    [Serializable]
    public class OutstockCreateDto : IMultiStore, IOutstock
    {
        [DisplayName("OutstockOutstockTime")]
        public DateTime OutstockTime { get; set; }

        [DisplayName("OutstockProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [DisplayName("OutstockUnitPrice")]
        public decimal UnitPrice { get; set; }

        [DisplayName("OutstockUnits")]
        public int Units { get; set; }

        [DisplayName("OutstockOperatorName")]
        public string OperatorName { get; set; }

        [DisplayName("OutstockDescription")]
        public string Description { get; set; }

        [DisplayName("OutstockWarehouseId")]
        public Guid WarehouseId { get; set; }

        [DisplayName("OutstockStoreId")]
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}