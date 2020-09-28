using System;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    [Dependency(TryRegister = true)]
    public class HttpClientCouponLookupService : ICouponLookupService, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICouponAppService _couponAppService;

        public HttpClientCouponLookupService(
            IObjectMapper objectMapper,
            ICouponAppService couponAppService)
        {
            _objectMapper = objectMapper;
            _couponAppService = couponAppService;
        }
        
        public virtual async Task<CouponData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _objectMapper.Map<CouponDto, CouponData>(await _couponAppService.GetAsync(id));
        }
    }
}