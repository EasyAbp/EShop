using System;
using EasyAbp.EShop.Plugins.FlashSales.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class MongoFlashSaleResultRepository : MongoDbRepository<IFlashSalesMongoDbContext, FlashSaleResult, Guid>, IFlashSaleResultRepository
{
    public MongoFlashSaleResultRepository(IMongoDbContextProvider<IFlashSalesMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
