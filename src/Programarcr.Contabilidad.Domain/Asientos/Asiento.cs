using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Periodos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.PermissionManagement;

namespace Programarcr.Contabilidad.Asientos
{
    public class Asiento : FullAuditedAggregateRoot<int>
    {
        public PeriodoContable Periodo { get; protected set; }
        public DateTime Fecha { get; protected set; }
        public string Descripcion { get; set; }
        public ICollection<Linea> Lineas { get; protected set; }
        public bool Borrador { get; protected set; }
        protected Asiento()
        {
            Lineas = new Collection<Linea>();
            Borrador = true;
        }

        public Asiento(DateTime fecha, int periodoId): this()
        {
            Validacion.NotNull(fecha, nameof(fecha));
            Validacion.NotNull(periodoId, nameof(periodoId));
            Fecha = fecha;
        }

        public Linea AddLinea(string cuentaId, string descripcion, string monedaId, BalanceTypeEnum balance, decimal cambio, decimal monto)
        {
            if (!Borrador)
                throw new AsientoAplicadoExeption();

            int numero = Lineas.Count + 1;
            Linea linea = new(cuentaId, numero, descripcion,monedaId,balance, cambio, monto);
            Lineas.Add(linea);

            return linea;
        }
    }
}
