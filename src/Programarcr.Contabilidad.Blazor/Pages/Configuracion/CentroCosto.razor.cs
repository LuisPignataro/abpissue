using Programarcr.Contabilidad.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.BlazoriseUI;

namespace Programarcr.Contabilidad.Blazor.Pages.Configuracion
{
    public partial class CentroCostoBase: AbpCrudPageBase<ICentroCostoAppService, CentroCostoDto, int>
    {
    }
}
