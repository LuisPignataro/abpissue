using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Programarcr.Contabilidad.Cuentas
{
    public interface ICuentasAppService : IApplicationService
    {
        Task<Collection<CuentaDto>> GetChildListAsync(string id);
        Task<List<CuentaDto>> GetRootListAsync();
        Task<List<CreateCuentaResultDto>> CrearCuentas(List<CreateCuentaDto> input);
        Task<CreateCuentaResultDto> CrearCuenta(CreateCuentaDto input);
        bool AlreadyExistsAccount(string numero);
        Task<PagedResultDto<CuentaDto>> GetListAsync(CuentaSearchInput input);
    }
}
