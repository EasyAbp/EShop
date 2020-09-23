using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface ICouponTemplateRepository : IRepository<CouponTemplate, Guid>
    {
    }
}