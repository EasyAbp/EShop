using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace EasyMall.EntityFrameworkCore
{
    public static class EasyMallDbContextModelCreatingExtensions
    {
        public static void ConfigureEShopEasyMall(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(EasyMallConsts.DbTablePrefix + "YourEntities", EasyMallConsts.DbSchema);

            //    //...
            //});
        }
    }
}