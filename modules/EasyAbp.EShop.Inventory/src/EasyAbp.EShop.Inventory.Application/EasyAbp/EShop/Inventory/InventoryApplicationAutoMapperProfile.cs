using AutoMapper;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Instocks.Dtos;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Outstocks.Dtos;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.Reallocations.Dtos;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.StockHistories.Dtos;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Stocks.Dtos;
using EasyAbp.EShop.Inventory.Suppliers;
using EasyAbp.EShop.Inventory.Suppliers.Dtos;
using EasyAbp.EShop.Inventory.Warehouses;
using EasyAbp.EShop.Inventory.Warehouses.Dtos;

namespace EasyAbp.EShop.Inventory
{
    public class InventoryApplicationAutoMapperProfile : Profile
    {
        public InventoryApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Instock, InstockDto>();
            CreateMap<InstockCreateDto, Instock>(MemberList.Source);
            CreateMap<InstockUpdateDto, Instock>(MemberList.Source);
            CreateMap<Outstock, OutstockDto>();
            CreateMap<OutstockCreateDto, Outstock>(MemberList.Source);
            CreateMap<OutstockUpdateDto, Outstock>(MemberList.Source);
            CreateMap<Reallocation, ReallocationDto>();
            CreateMap<ReallocationCreateDto, Reallocation>(MemberList.Source);
            CreateMap<ReallocationUpdateDto, Reallocation>(MemberList.Source);
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<WarehouseCreateDto, Warehouse>(MemberList.Source);
            CreateMap<WarehouseUpdateDto, Warehouse>(MemberList.Source);
            CreateMap<Stock, StockDto>();
            CreateMap<StockCreateDto, Stock>(MemberList.Source);
            CreateMap<StockUpdateDto, Stock>(MemberList.Source);
            CreateMap<StockHistory, StockHistoryDto>();
            CreateMap<StockHistoryCreateDto, StockHistory>(MemberList.Source);
            CreateMap<StockHistoryUpdateDto, StockHistory>(MemberList.Source);
            CreateMap<Supplier, SupplierDto>();
            CreateMap<SupplierCreateDto, Supplier>(MemberList.Source);
            CreateMap<SupplierUpdateDto, Supplier>(MemberList.Source);
        }
    }
}