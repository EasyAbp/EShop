using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[ConnectionStringName(FlashSalesDbProperties.ConnectionStringName)]
public class FlashSalesMongoDbContext : AbpMongoDbContext, IFlashSalesMongoDbContext
{
    public IMongoCollection<FlashSalePlan> FlashSalePlans => Collection<FlashSalePlan>();

    public IMongoCollection<FlashSaleResult> FlashSaleResults => Collection<FlashSaleResult>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureEShopPluginsFlashSales();
    }
}
