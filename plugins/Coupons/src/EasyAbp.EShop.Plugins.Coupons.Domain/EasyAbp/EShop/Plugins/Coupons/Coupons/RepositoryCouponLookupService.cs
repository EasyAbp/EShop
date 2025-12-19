using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class RepositoryCouponLookupService : ICouponLookupService, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICouponRepository _couponRepository;

        public RepositoryCouponLookupService(
            IObjectMapper<EShopPluginsCouponsDomainModule> objectMapper,
            ICouponRepository couponRepository)
        {
            _objectMapper = objectMapper;
            _couponRepository = couponRepository;
        }
        
        public virtual async Task<CouponData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _objectMapper.Map<Coupon, CouponData>(
                await _couponRepository.FindAsync(id, true, cancellationToken));
        }
    }
}