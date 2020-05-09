﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Stores.MongoDB
{
    [ConnectionStringName(StoresDbProperties.ConnectionStringName)]
    public class StoresMongoDbContext : AbpMongoDbContext, IStoresMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEShopStores();
        }
    }
}