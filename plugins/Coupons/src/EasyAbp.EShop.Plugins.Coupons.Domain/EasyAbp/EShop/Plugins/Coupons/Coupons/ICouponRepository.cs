using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponRepository : IRepository<Coupon, Guid>
    {
    }
}