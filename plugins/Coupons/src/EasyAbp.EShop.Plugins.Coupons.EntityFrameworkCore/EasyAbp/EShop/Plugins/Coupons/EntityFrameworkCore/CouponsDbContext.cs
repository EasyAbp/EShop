using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.Coupons;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    [ConnectionStringName(CouponsDbProperties.ConnectionStringName)]
    public class CouponsDbContext : AbpDbContext<CouponsDbContext>, ICouponsDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<CouponTemplate> CouponTemplates { get; set; }
        public DbSet<CouponTemplateScope> CouponTemplateScopes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        public CouponsDbContext(DbContextOptions<CouponsDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureEShopPluginsCoupons();
        }
    }
}
