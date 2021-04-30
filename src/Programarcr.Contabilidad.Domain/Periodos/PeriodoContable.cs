using Programarcr.Contabilidad.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Programarcr.Contabilidad.Periodos
{
    public class PeriodoContable : FullAuditedAggregateRoot<int>
    {
        protected PeriodoContable()
        {

        }

        public PeriodoContable(MesContable inicio, MesContable fin)
        {
            Inicio = inicio;
            Fin = fin;
        }

        public int Codigo
        {
            get
            {
                return Inicio?.Año ?? 0;
            }
        }
        public MesContable Inicio { get; set; }
        public MesContable Fin { get; set; }

        public bool Activo { get; set; }
    }
}
