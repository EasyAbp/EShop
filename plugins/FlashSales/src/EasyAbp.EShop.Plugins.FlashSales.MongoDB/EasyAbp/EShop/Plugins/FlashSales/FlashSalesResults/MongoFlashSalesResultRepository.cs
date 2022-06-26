using System;
using EasyAbp.EShop.Plugins.FlashSales.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class MongoFlashSalesResultRepository : MongoDbRepository<IFlashSalesMongoDbContext, FlashSalesResult, Guid>, IFlashSalesResultRepository
{
    public MongoFlashSalesResultRepository(IMongoDbContextProvider<IFlashSalesMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
