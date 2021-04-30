using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Data
{
    public interface IContabilidadDbSchemaMigrator
    {
        Task<bool> CreateDatabase();
        Task MigrateAsync();
    }
}
