using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class MonedaFilterDto: PagedAndSortedResultRequestDto
    {
        public bool IsEnabled { get; set; } = true;
    }
}
