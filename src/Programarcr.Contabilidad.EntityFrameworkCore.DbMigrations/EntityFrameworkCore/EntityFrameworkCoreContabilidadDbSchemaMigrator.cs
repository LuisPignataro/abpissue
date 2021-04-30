using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Programarcr.Contabilidad.Data;
using Volo.Abp.DependencyInjection;

namespace Programarcr.Contabilidad.EntityFrameworkCore
{
    public class EntityFrameworkCoreContabilidadDbSchemaMigrator
        : IContabilidadDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreContabilidadDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> CreateDatabase()
        {
            return await _serviceProvider
                .GetRequiredService<ContabilidadMigrationsDbContext>()
                .Database
                .EnsureCreatedAsync();

        }
        public async Task MigrateAsync()
        {
            /* We intentionally resolving the ContabilidadMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<ContabilidadMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}