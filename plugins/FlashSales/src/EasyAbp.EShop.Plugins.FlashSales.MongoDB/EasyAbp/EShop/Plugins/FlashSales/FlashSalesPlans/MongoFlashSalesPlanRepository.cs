using System;
using EasyAbp.EShop.Plugins.FlashSales.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public class MongoFlashSalesPlanRepository : MongoDbRepository<IFlashSalesMongoDbContext, FlashSalesPlan, Guid>, IFlashSalesPlanRepository
{
    public MongoFlashSalesPlanRepository(IMongoDbContextProvider<IFlashSalesMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
