using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateRepository : EfCoreRepository<ICouponsDbContext, CouponTemplate, Guid>, ICouponTemplateRepository
    {
        public CouponTemplateRepository(IDbContextProvider<ICouponsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<CouponTemplate>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).Include(x => x.Scopes);
        }
    }
}