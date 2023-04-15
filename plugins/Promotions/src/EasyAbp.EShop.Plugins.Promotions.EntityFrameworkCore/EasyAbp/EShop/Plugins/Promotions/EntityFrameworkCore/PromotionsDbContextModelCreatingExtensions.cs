using EasyAbp.EShop.Plugins.Promotions.Promotions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore;

public static class PromotionsDbContextModelCreatingExtensions
{
    public static void ConfigureEShopPluginsPromotions(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(PromotionsDbProperties.DbTablePrefix + "Questions", PromotionsDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */


        builder.Entity<Promotion>(b =>
        {
            b.ToTable(PromotionsDbProperties.DbTablePrefix + "Promotions", PromotionsDbProperties.DbSchema);
            b.ConfigureByConvention(); 
            

            /* Configure more properties here */
        });
    }
}
