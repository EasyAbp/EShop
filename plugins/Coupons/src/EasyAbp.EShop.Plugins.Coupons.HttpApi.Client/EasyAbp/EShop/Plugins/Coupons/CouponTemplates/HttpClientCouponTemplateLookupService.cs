using System;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    [Dependency(TryRegister = true)]
    public class HttpClientCouponTemplateLookupService : ICouponTemplateLookupService, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICouponTemplateAppService _couponTemplateAppService;

        public HttpClientCouponTemplateLookupService(
            IObjectMapper objectMapper,
            ICouponTemplateAppService couponTemplateAppService)
        {
            _objectMapper = objectMapper;
            _couponTemplateAppService = couponTemplateAppService;
        }
        
        public virtual async Task<CouponTemplateData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _objectMapper.Map<CouponTemplateDto, CouponTemplateData>(
                await _couponTemplateAppService.GetAsync(id));
        }
    }
}