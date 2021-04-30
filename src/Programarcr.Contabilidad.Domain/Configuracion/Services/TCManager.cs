using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Configuracion.Services
{
    public class TCManager: DomainService, ITCManager
    {
        private IRepository<TipoCambio, int> _store;

        public TCManager(IRepository<TipoCambio, int> store)
        {
            _store = store;
        }

        public async Task<TipoCambio> GetAsync(string monedaId)
        {
            return await _store.OrderBy(x=> x.Id).LastOrDefaultAsync(x=> x.MonedaId == monedaId);
        }

        public async Task<TipoCambio> GetAsync(string monedaId, DateTime fecha)
        {
            return await _store.OrderBy(x => x.Id).LastOrDefaultAsync(x => x.MonedaId == monedaId && x.Fecha <= fecha);
        }

        public async Task PushAsync(string monedaId, decimal compra, decimal venta, DateTime? fecha = null)
        {
            var last = await GetAsync(monedaId);

            if(last == null || last.Compra != compra || last.Venta != venta)
            {
                if (!fecha.HasValue)
                    fecha = DateTime.Now;

                await _store.InsertAsync(new TipoCambio
                {
                    MonedaId = monedaId,
                    Compra = compra,
                    Venta = venta,
                    Fecha = fecha.Value
                }, autoSave: true);
            }
        }
    }
}
