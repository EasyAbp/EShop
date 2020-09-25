using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class RepositoryCouponLookupService : ICouponLookupService, ITransientDependency
    {
        private readonly ICouponRepository _couponRepository;

        public RepositoryCouponLookupService(
            ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        
        public virtual async Task<ICoupon> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _couponRepository.FindAsync(id, true, cancellationToken);
        }
    }
}