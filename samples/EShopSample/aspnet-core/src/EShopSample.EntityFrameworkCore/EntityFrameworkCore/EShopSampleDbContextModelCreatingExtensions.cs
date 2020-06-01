using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EShopSample.EntityFrameworkCore
{
    public static class EShopSampleDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopSample(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EShopSampleConsts.DbTablePrefix + "YourEntities", EShopSampleConsts.DbSchema);

            //    //...
            //});
        }
    }
}