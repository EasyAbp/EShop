using EasyAbp.EShop.Inventory.Stocks;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class CreateProductSkuStockEto : IMultiTenant, IStock
    {

        public Guid? TenantId { get; set; }

        public int LockedQuantity { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsEnabled { get; set; }

        public string Description { get; set; }

        public ExtraPropertyDictionary ExtraProperties { get; set; }

        public Guid StoreId { get; set; }

        public List<Guid> WarehouseIds { get; set; }

        public Guid ProductId { get; set; }
    }
}
