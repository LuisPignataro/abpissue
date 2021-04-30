using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad.Configuracion
{
    public interface IMonedaAppService : IReadOnlyAppService<MonedaDto, MonedaDto , string, MonedaFilterDto>
    {
        Task Disable(string id);
        Task Enable(string id);
        Task SetDefault(string id);
        Task<MonedaDto> GetDefault();
    }
}
