﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.Baskets.MongoDB
{
    [ConnectionStringName(BasketsDbProperties.ConnectionStringName)]
    public interface IBasketsMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
