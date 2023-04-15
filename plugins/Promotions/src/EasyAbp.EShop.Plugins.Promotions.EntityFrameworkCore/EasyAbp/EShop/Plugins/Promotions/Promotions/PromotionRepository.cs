using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class PromotionRepository : EfCoreRepository<IPromotionsDbContext, Promotion, Guid>, IPromotionRepository
{
    public PromotionRepository(IDbContextProvider<IPromotionsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Promotion>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}