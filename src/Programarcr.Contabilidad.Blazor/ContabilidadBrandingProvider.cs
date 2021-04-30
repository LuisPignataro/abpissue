using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Programarcr.Contabilidad.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class ContabilidadBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Contabilidad";
    }
}
