using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans
{
    public class EfCoreFlashSalePlanRepository : EfCoreRepository<IFlashSalesDbContext, FlashSalePlan, Guid>, IFlashSalePlanRepository
    {
        public EfCoreFlashSalePlanRepository(IDbContextProvider<IFlashSalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<FlashSalePlan>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
