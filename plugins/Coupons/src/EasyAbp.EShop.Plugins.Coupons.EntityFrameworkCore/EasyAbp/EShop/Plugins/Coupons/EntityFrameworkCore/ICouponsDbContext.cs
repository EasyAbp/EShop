using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.Coupons;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public interface ICouponsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<CouponTemplate> CouponTemplates { get; set; }
        DbSet<CouponTemplateScope> CouponTemplateScopes { get; set; }
        DbSet<Coupon> Coupons { get; set; }
    }
}
