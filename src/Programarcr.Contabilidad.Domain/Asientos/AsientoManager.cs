using Programarcr.Contabilidad.Periodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Asientos
{
    public class AsientoManager: DomainService, IAsientoManager
    {
        private IPeriodoContableManager _periodoContableManager;
        private IRepository<Asiento, int> _asientosStore;

        public AsientoManager(IPeriodoContableManager periodoContableManager, IRepository<Asiento, int> asientosStore)
        {
            _periodoContableManager = periodoContableManager;
            _asientosStore = asientosStore;
        }

        public async Task<Asiento> CreateAsync(DateTime fecha)
        {
            var periodo = await _periodoContableManager.GetByDateAsync(fecha);
            var asiento =  new Asiento(fecha, periodo.Id);

            await _asientosStore.InsertAsync(asiento, autoSave: true);

            return asiento;
        }
    }
}
