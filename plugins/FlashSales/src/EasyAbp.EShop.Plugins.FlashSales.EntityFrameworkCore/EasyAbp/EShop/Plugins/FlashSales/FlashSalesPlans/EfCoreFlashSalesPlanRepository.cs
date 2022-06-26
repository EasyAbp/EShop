using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans
{
    public class EfCoreFlashSalesPlanRepository : EfCoreRepository<IFlashSalesDbContext, FlashSalesPlan, Guid>, IFlashSalesPlanRepository
    {
        public EfCoreFlashSalesPlanRepository(IDbContextProvider<IFlashSalesDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<FlashSalesPlan>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
