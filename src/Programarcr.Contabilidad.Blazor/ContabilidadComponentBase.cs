using Programarcr.Contabilidad.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Programarcr.Contabilidad.Blazor
{
    public abstract class ContabilidadComponentBase : AbpComponentBase
    {
        protected ContabilidadComponentBase()
        {
            LocalizationResource = typeof(ContabilidadResource);
        }
    }
}
