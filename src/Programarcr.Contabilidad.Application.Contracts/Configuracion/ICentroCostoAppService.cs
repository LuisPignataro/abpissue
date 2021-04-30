using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad.Configuracion
{
    public interface ICentroCostoAppService: ICrudAppService<CentroCostoDto, int>
    {
    }
}
