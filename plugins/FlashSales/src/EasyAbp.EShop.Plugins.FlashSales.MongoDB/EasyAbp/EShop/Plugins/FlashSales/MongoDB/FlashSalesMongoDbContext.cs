using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public class FlashSalesMongoDbContext : AbpMongoDbContext, IFlashSalesMongoDbContext
{
    public IMongoCollection<FlashSalesPlan> Plans => Collection<FlashSalesPlan>();

    public IMongoCollection<FlashSalesResult> Results => Collection<FlashSalesResult>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFlashSales();
    }
}
