using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponRepository : IRepository<Coupon, Guid>
    {
        Task<IQueryable<Coupon>> GetAvailableCouponQueryableAsync(IClock clock);
    }
}