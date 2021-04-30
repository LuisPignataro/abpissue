using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Programarcr.Contabilidad.EntityFrameworkCore
{
    [DependsOn(
        typeof(ContabilidadEntityFrameworkCoreModule)
        )]
    public class ContabilidadEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ContabilidadMigrationsDbContext>();
        }
    }
}
