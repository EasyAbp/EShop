using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public interface IFlashSalesMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<FlashSalePlan> FlashSalePlans { get; }

    IMongoCollection<FlashSaleResult> FlashSaleResults { get; }
}
