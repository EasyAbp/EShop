using EasyAbp.EShop.Inventory.Suppliers;
using EasyAbp.EShop.Inventory.Instocks;
using EasyAbp.EShop.Inventory.Outstocks;
using EasyAbp.EShop.Inventory.Reallocations;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.Stocks;
using EasyAbp.EShop.Inventory.Warehouses;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore
{
    [DependsOn(
        typeof(InventoryDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class InventoryEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<InventoryDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Instock, InstockRepository>();
                options.AddRepository<Outstock, OutstockRepository>();
                options.AddRepository<Reallocation, ReallocationRepository>();
                options.AddRepository<Warehouse, WarehouseRepository>();
                options.AddRepository<Stock, StockRepository>();
                options.AddRepository<StockHistory, StockHistoryRepository>();
                options.AddRepository<Supplier, SupplierRepository>();
            });
        }
    }
}
