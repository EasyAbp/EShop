using AutoMapper;
using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Inventory.Stocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.EShop.Inventory
{

    public class InventoryDomainAutoMapperProfile : Profile
    {
        public InventoryDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Stock, StockEto>().ReverseMap();
            CreateMap<Stock, CreateProductSkuStockEto>().ReverseMap();
        }
    }
}
