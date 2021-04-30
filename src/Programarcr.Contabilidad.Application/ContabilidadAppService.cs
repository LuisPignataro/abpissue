using System;
using System.Collections.Generic;
using System.Text;
using Programarcr.Contabilidad.Localization;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad
{
    /* Inherit your application services from this class.
     */
    public abstract class ContabilidadAppService : ApplicationService
    {
        protected ContabilidadAppService()
        {
            LocalizationResource = typeof(ContabilidadResource);
        }
    }
}
