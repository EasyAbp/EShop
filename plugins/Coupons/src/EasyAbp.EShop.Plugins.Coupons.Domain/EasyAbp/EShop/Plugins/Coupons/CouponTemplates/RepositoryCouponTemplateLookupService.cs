using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class RepositoryCouponTemplateLookupService : ICouponTemplateLookupService, ITransientDependency
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ICouponTemplateRepository _couponTemplateRepository;

        public RepositoryCouponTemplateLookupService(
            IObjectMapper objectMapper,
            ICouponTemplateRepository couponTemplateRepository)
        {
            // Todo: use ICouponTemplateStore
            _objectMapper = objectMapper;
            _couponTemplateRepository = couponTemplateRepository;
        }
        
        public virtual async Task<CouponTemplateData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _objectMapper.Map<CouponTemplate, CouponTemplateData>(
                await _couponTemplateRepository.FindAsync(id, true, cancellationToken));
        }
    }
}