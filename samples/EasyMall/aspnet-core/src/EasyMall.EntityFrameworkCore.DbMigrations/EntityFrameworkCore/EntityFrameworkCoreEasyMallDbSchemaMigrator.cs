using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EasyMall.Data;
using Volo.Abp.DependencyInjection;

namespace EasyMall.EntityFrameworkCore
{
    public class EntityFrameworkCoreEasyMallDbSchemaMigrator
        : IEasyMallDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreEasyMallDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the EasyMallMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<EasyMallMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}