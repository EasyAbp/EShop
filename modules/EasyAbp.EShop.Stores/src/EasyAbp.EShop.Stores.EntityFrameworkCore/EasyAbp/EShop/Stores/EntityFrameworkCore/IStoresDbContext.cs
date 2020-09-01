using EasyAbp.EShop.Stores.StoreOwners;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Transactions;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore
{
    [ConnectionStringName(StoresDbProperties.ConnectionStringName)]
    public interface IStoresDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Store> Stores { get; set; }

        DbSet<StoreOwner> StoreOwners { get; set; }
        DbSet<Transaction> Transactions { get; set; }
    }
}
