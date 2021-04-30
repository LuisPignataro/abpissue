using Volo.Abp.Bundling;

namespace Programarcr.Contabilidad.Blazor
{
    public class ContabilidadBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css");
        }
    }
}
