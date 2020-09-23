using System;
using EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponRepository : EfCoreRepository<ICouponsDbContext, Coupon, Guid>, ICouponRepository
    {
        public CouponRepository(IDbContextProvider<ICouponsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}