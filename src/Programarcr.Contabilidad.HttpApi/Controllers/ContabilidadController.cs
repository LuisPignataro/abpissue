using Programarcr.Contabilidad.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Programarcr.Contabilidad.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class ContabilidadController : AbpController
    {
        protected ContabilidadController()
        {
            LocalizationResource = typeof(ContabilidadResource);
        }
    }
}