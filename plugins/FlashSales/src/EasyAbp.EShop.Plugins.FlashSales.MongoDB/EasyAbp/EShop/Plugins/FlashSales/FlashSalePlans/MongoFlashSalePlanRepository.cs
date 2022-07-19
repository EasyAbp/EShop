using System;
using EasyAbp.EShop.Plugins.FlashSales.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public class MongoFlashSalePlanRepository : MongoDbRepository<IFlashSalesMongoDbContext, FlashSalePlan, Guid>, IFlashSalePlanRepository
{
    public MongoFlashSalePlanRepository(IMongoDbContextProvider<IFlashSalesMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
