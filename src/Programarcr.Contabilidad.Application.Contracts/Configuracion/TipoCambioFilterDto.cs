using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class TipoCambioFilterDto : PagedAndSortedResultRequestDto
    {
        public string MonedaId { get; set; }
    }
}