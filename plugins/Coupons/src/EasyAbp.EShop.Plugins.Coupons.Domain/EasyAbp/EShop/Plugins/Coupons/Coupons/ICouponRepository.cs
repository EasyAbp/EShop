using System;
using System.Linq;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponRepository : IRepository<Coupon, Guid>
    {
        IQueryable<Coupon> GetAvailableCouponQueryable(IClock clock);
    }
}