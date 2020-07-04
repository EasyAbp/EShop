using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public interface IBasketsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<BasketItem> BasketItems { get; set; }
        DbSet<ProductUpdate> ProductUpdates { get; set; }
    }
}
