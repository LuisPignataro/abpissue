using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Configuracion.Services
{

    public class MonedaManager: DomainService, IMonedaManager
    {
        private readonly IRepository<Moneda, string> _monedaStore;

        public MonedaManager(IRepository<Moneda, string> monedaStore)
        {
            _monedaStore = monedaStore;
        }
        public virtual async Task<Moneda> GetAsync(string id)
        {
            Check.NotNullOrWhiteSpace(id, nameof(id));

            var moneda = await _monedaStore.FirstOrDefaultAsync(x => x.IsEnabled  && x.Id == id);

            if (moneda == null)
                throw new MonedaNotFountExeption(id);

            return moneda;
        }

        public virtual async Task<Moneda> GetDefault()
        {
            return await _monedaStore.FirstOrDefaultAsync(x => x.IsDefault);
        }

        public virtual async Task SetDefault(string id)
        {
            var moneda = await _monedaStore.GetAsync(id);
            
            if (!moneda.IsEnabled)
                throw new MonedaNotEnabledExeption(id);

            var monedaDefault = await _monedaStore.FirstOrDefaultAsync(x => x.IsDefault == true);

            if (monedaDefault != null && moneda != null && monedaDefault.Id != id)
            {
                monedaDefault.IsDefault = false;
            }

            moneda.IsDefault = true;
        }

        public virtual async Task SetRate(string id, decimal compra, decimal venta, DateTime fecha)
        {
            var moneda = await _monedaStore.GetAsync(id);
            moneda.Cambio = venta;

        }
    }
}
