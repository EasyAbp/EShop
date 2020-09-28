using System;
using System.Linq;
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

        public override IQueryable<CouponTemplate> WithDetails()
        {
            return base.WithDetails().Include(x => x.Scopes);
        }
    }
}