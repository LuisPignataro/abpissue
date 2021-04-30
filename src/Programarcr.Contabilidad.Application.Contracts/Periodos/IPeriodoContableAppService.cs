using System.Threading.Tasks;
using Programarcr.Contabilidad.Periodos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad.Periodos
{
    public interface IPeriodoContableAppService : 
        ICrudAppService<PeriodoContableDto, int, PagedAndSortedResultRequestDto, PeriodoContableCreateInput>
    {
        Task<PeriodoContableDto> GetLastAsync();
    }
}