using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface ICouponTemplateLookupService
    {
        Task<CouponTemplateData> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}