using EasyAbp.EShop.Inventory.Instocks.Dtos;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using EasyAbp.EShop.Inventory.Reallocations.Dtos;
using EasyAbp.EShop.Inventory.StockHistories.Dtos;
using EasyAbp.EShop.Inventory.Stocks.Dtos;
using EasyAbp.EShop.Stores.Stores;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Warehouses.Dtos
{
    [Serializable]
    public class WarehouseDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhoneNumber { get; set; }

        public Guid StoreId { get; set; }

        public List<StockDto> Stocks { get; set; }

        public List<StockHistoryDto> StockHistories { get; set; }

        public List<InstockDto> Instocks { get; set; }

        public List<OutstockDto> Outstocks { get; set; }

        public List<ReallocationDto> Reallocations { get; set; }
    }
}