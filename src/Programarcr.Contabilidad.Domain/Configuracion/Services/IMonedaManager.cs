using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Configuracion.Services
{
    public interface IMonedaManager : IDomainService
    {
        Task<Moneda> GetAsync(string id);
        Task<Moneda> GetDefault();
        Task SetDefault(string id);
        Task SetRate(string id, decimal compra, decimal venta, DateTime fecha);
    }
}
