using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad.Configuracion
{
    public interface ITipoCambioAppService : ICrudAppService<TipoCambioDto, int, TipoCambioFilterDto, CreateUpdateTipoCambioDto>
    {
    }
}