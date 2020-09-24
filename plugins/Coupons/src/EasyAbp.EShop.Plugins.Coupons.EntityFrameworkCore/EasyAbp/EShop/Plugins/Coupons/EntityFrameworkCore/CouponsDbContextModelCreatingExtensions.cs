using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore
{
    public static class CouponsDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopPluginsCoupons(
            this ModelBuilder builder,
            Action<CouponsModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CouponsModelBuilderConfigurationOptions(
                CouponsDbProperties.DbTablePrefix,
                CouponsDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */


            builder.Entity<CouponTemplate>(b =>
            {
                b.ToTable(options.TablePrefix + "CouponTemplates", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
                b.Property(x => x.ConditionAmount).HasColumnType("decimal(20,8)");
                b.Property(x => x.DiscountAmount).HasColumnType("decimal(20,8)");
                b.HasMany(typeof(CouponTemplateScope), nameof(CouponTemplate.Scopes));
            });
            
            builder.Entity<CouponTemplateScope>(b =>
            {
                b.ToTable(options.TablePrefix + "CouponTemplateScopes", options.Schema);
                b.ConfigureByConvention();

                /* Configure more properties here */
            });

            builder.Entity<Coupon>(b =>
            {
                b.ToTable(options.TablePrefix + "Coupons", options.Schema);
                b.ConfigureByConvention(); 
                
                /* Configure more properties here */
                b.Property(x => x.DiscountedAmount).HasColumnType("decimal(20,8)");
            });
        }
    }
}
