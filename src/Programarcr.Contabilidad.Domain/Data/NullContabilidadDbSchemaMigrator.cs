using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Programarcr.Contabilidad.Data
{
    /* This is used if database provider does't define
     * IContabilidadDbSchemaMigrator implementation.
     */
    public class NullContabilidadDbSchemaMigrator : IContabilidadDbSchemaMigrator, ITransientDependency
    {
        public Task<bool> CreateDatabase()
        {
            return Task.FromResult(false);
        }

        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}