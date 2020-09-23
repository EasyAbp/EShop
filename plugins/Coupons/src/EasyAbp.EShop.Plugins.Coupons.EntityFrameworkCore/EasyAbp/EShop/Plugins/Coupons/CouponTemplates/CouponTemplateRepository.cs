using System;
using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateRepository : EfCoreRepository<ICouponsDbContext, CouponTemplate, Guid>, ICouponTemplateRepository
    {
        public CouponTemplateRepository(IDbContextProvider<ICouponsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}