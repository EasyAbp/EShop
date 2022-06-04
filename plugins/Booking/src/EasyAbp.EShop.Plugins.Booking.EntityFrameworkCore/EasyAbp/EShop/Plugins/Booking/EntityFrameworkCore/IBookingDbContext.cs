using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;

[ConnectionStringName(BookingDbProperties.ConnectionStringName)]
public interface IBookingDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
        DbSet<ProductAsset> ProductAssets { get; set; }
        DbSet<ProductAssetPeriod> ProductAssetPeriods { get; set; }
        DbSet<ProductAssetCategoryPeriod> ProductAssetCategoryPeriods { get; set; }
        DbSet<ProductAssetCategory> ProductAssetCategories { get; set; }
        DbSet<GrantedStore> GrantedStores { get; set; }
}
