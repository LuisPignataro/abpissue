using Volo.Abp.Modularity;

namespace Programarcr.Contabilidad
{
    [DependsOn(
        typeof(ContabilidadApplicationModule),
        typeof(ContabilidadDomainTestModule)
        )]
    public class ContabilidadApplicationTestModule : AbpModule
    {

    }
}