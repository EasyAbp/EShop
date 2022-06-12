using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore;

public static class FlashSalesDbContextModelCreatingExtensions
{
    public static void ConfigureFlashSales(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(FlashSalesDbProperties.DbTablePrefix + "Questions", FlashSalesDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
    }
}
