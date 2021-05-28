using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Inventory.Suppliers;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    [ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
    public interface IInventoryDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Instock> Instocks { get; set; }
        DbSet<Outstock> Outstocks { get; set; }
        DbSet<Reallocation> Reallocations { get; set; }
        DbSet<StockHistory> InventoryHistories { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<Stock> Stocks { get; set; }
        DbSet<StockHistory> StockHistories { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
    }
}
