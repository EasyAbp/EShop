using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                    DbContext.CouponTemplates.Where(x => !x.UsableBeginTime.HasValue || x.UsableBeginTime.Value <= now),
                    coupon => coupon.CouponTemplateId,
                    couponTemplate => couponTemplate.Id,
                    (coupon, couponTemplate) => coupon
                );
        }

        public override async Task<Coupon> InsertAsync(Coupon entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            var clock = ServiceProvider.GetRequiredService<IClock>();

            var notExpiredCouponQuantity =
                await (await GetDbSetAsync()).CountAsync(x => x.UserId == entity.UserId && x.ExpirationTime > clock.Now,
                    cancellationToken);
            
            if (notExpiredCouponQuantity >= CouponsConsts.MaxNotExpiredCouponQuantityPerUser)
            {
                throw new UserCouponQuantityExceedsLimitException(CouponsConsts.MaxNotExpiredCouponQuantityPerUser);
            }

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }
    }
}