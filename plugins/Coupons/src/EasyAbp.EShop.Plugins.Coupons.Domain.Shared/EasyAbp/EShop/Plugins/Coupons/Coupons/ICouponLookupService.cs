using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponLookupService
    {
        Task<CouponData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}