using Programarcr.Contabilidad.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Programarcr.Contabilidad
{
    [DependsOn(
        typeof(ContabilidadEntityFrameworkCoreTestModule)
        )]
    public class ContabilidadDomainTestModule : AbpModule
    {

    }
}