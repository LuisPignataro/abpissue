using Programarcr.Contabilidad.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Periodos
{
    public interface IPeriodoContableManager
    {
        Task<PeriodoContable> CreateAsync(MesContable inicio, MesContable fin);
        Task<PeriodoContable> GetByDateAsync(DateTime fecha);
        Task<PeriodoContable> GetLastAsync();
    }
}
