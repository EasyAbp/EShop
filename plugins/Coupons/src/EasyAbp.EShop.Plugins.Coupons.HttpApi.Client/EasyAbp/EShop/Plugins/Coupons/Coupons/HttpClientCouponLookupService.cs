using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    [Dependency(TryRegister = true)]
    public class HttpClientCouponLookupService : ICouponLookupService, ITransientDependency
    {
        private readonly ICouponAppService _couponAppService;

        public HttpClientCouponLookupService(
            ICouponAppService couponAppService)
        {
            _couponAppService = couponAppService;
        }
        
        public virtual async Task<ICoupon> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _couponAppService.GetAsync(id);
        }
    }
}