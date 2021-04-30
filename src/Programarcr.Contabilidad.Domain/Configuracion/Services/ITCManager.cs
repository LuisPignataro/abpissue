using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Configuracion.Services
{
    public interface ITCManager : IDomainService
    {
        Task<TipoCambio> GetAsync(string monedaId);
        Task<TipoCambio> GetAsync(string monedaId, DateTime fecha);
        Task PushAsync(string monedaId, decimal compra, decimal venta, DateTime? fecha = null);
    }
}
