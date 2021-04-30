using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Programarcr.Contabilidad.ValueObjects;
using Volo.Abp;
using System.Security.Cryptography.X509Certificates;

namespace Programarcr.Contabilidad.Periodos
{
    public class PeriodoContableManager: DomainService, IPeriodoContableManager
    {
        private IRepository<PeriodoContable> _periodosStore;

        public PeriodoContableManager(IRepository<PeriodoContable> periodosStore)
        {
            _periodosStore = periodosStore;
        }

        public async Task<PeriodoContable> CreateAsync(MesContable inicio, MesContable fin)
        {
            var last = await GetLastAsync();

            if(last != null)
            {
                if (! inicio.Equals(last.Fin + 1))
                    throw new ArgumentException("El inicio debe ser un mes contiguo al último periodo");
            }

            PeriodoContable periodo = new PeriodoContable(inicio, fin);

            await _periodosStore.InsertAsync(periodo, autoSave: true);

            return periodo;
        }

        public async Task<PeriodoContable> GetLastAsync()
        {
            try
            {
                var queriable = await _periodosStore.GetQueryableAsync();
                PeriodoContable periodo = await queriable.OrderBy(x=> x.Id).LastAsync();
                return periodo;
            }
            catch
            {
                return null;
            }

        }
    
        public async Task<PeriodoContable> GetByDateAsync(DateTime fecha)
        {
            
            int mes = fecha.Month;
            int año = fecha.Year;
            var queriable = await _periodosStore.GetQueryableAsync();
            PeriodoContable periodo = await _periodosStore
                .FirstOrDefaultAsync(x => 
                    //Mismo año
                    mes >= x.Inicio.Mes  && año == x.Inicio.Año && mes <= x.Fin.Mes  && año == x.Fin.Año ||
                    //Años diferentes
                    mes >= x.Inicio.Mes && año == x.Inicio.Año &&  año < x.Fin.Año ||
                    año > x.Inicio.Año && mes <= x.Fin.Mes && año == x.Fin.Año
                    );

            return periodo;
        }
    }
}
