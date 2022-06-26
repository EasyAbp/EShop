using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public interface IFlashSalesMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<FlashSalesPlan> Plans { get; }

    IMongoCollection<FlashSalesResult> Results { get; }
}
