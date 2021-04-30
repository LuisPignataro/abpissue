using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Cuentas
{
    public interface ICuentasManager : IDomainService
    {
        Task<Cuenta> CreateCuentaChildAsync(string parent, string numero, string monedaId, BalanceTypeEnum balance, string nombre);
        Task<Cuenta> CreateCuentaRootAsync(string numero, CuentaTypeEnum tipo, string monedaId, BalanceTypeEnum balance, string nombre);
        Task<ICollection<Cuenta>> GetCuetasChildAsync(string id);
        Task<IList<Cuenta>> GetCuetasRootAsync();
        bool ExistAnyCuenta();
    }
}
