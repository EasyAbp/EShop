using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EShopSample.Data;
using Volo.Abp.DependencyInjection;

namespace EShopSample.EntityFrameworkCore
{
    public class EntityFrameworkCoreEShopSampleDbSchemaMigrator
        : IEShopSampleDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEShopSampleDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EShopSampleMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EShopSampleMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}