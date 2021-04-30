using Programarcr.Contabilidad.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Programarcr.Contabilidad.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(ContabilidadEntityFrameworkCoreDbMigrationsModule),
        typeof(ContabilidadApplicationContractsModule)
        )]
    public class ContabilidadDbMigratorModule : AbpModule
    {
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
