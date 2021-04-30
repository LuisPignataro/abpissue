using Programarcr.Contabilidad.ValueObjects;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Periodos.Dtos
{
    public class PeriodoContableDto : FullAuditedEntityDto<int>
    {
        public MesContable Inicio { get; set; }
        public MesContable Fin { get; set; }
    }
}