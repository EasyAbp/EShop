using System;
using System.Linq;
using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponRepository : EfCoreRepository<ICouponsDbContext, Coupon, Guid>, ICouponRepository
    {
        public CouponRepository(IDbContextProvider<ICouponsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public virtual IQueryable<Coupon> GetAvailableCouponQueryable(IClock clock)
        {
            var now = clock.Now;

            return DbSet
                .Where(x => x.ExpirationTime > now)
                .Join(
                    DbContext.CouponTemplates.Where(x => x.UsableBeginTime <= now),
                    coupon => coupon.CouponTemplateId,
                    couponTemplate => couponTemplate.Id,
                    (coupon, couponTemplate) => coupon
                );
        }
    }
}