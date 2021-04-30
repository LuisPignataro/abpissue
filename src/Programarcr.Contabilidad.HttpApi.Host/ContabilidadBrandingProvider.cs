using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Programarcr.Contabilidad
{
    [Dependency(ReplaceServices = true)]
    public class ContabilidadBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Contabilidad";
    }
}
