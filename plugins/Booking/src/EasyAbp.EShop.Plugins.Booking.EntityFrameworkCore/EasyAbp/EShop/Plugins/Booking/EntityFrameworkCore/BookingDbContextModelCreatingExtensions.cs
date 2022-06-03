using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;

public static class BookingDbContextModelCreatingExtensions
{
    public static void ConfigureEShopPluginsBooking(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(BookingDbProperties.DbTablePrefix + "Questions", BookingDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */

            builder.Entity<ProductAsset>(b =>
            {
                b.ToTable(BookingDbProperties.DbTablePrefix + "ProductAssets", BookingDbProperties.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
                b.Property(x => x.Price).HasColumnType("decimal(20,8)");
            });

            builder.Entity<ProductAssetPeriod>(b =>
            {
                b.ToTable(BookingDbProperties.DbTablePrefix + "ProductAssetPeriods", BookingDbProperties.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
                b.Property(x => x.Price).HasColumnType("decimal(20,8)");
            });

            builder.Entity<ProductAssetCategoryPeriod>(b =>
            {
                b.ToTable(BookingDbProperties.DbTablePrefix + "ProductAssetCategoryPeriods", BookingDbProperties.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
                b.Property(x => x.Price).HasColumnType("decimal(20,8)");
            });

            builder.Entity<ProductAssetCategory>(b =>
            {
                b.ToTable(BookingDbProperties.DbTablePrefix + "ProductAssetCategories", BookingDbProperties.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
                b.Property(x => x.Price).HasColumnType("decimal(20,8)");
            });


            builder.Entity<StoreAssetCategory>(b =>
            {
                b.ToTable(BookingDbProperties.DbTablePrefix + "StoreAssetCategories", BookingDbProperties.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });
    }
}
