using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class EfCoreFlashSaleResultRepository : EfCoreRepository<IFlashSalesDbContext, FlashSaleResult, Guid>, IFlashSaleResultRepository
{
    public EfCoreFlashSaleResultRepository(IDbContextProvider<IFlashSalesDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<FlashSaleResult>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
