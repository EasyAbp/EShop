using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class EfCoreFlashSalesResultRepository : EfCoreRepository<IFlashSalesDbContext, FlashSalesResult, Guid>, IFlashSalesResultRepository
{
    public EfCoreFlashSalesResultRepository(IDbContextProvider<IFlashSalesDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<FlashSalesResult>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}
