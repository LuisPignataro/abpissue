using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Programarcr.Contabilidad.Asientos
{
    public class Linea : FullAuditedEntity<int>
    {
        public int AsientoId { get; protected set; }
        public int Numero { get; protected set; }

        public string CuentaId { get; protected set; }
        public Cuenta Cuenta { get; set; }
        public string Descripcion { get; protected set; }
        public BalanceTypeEnum Balance { get; protected set; }
        public string MonedaId { get; protected set; }
        public Moneda Moneda { get; protected set; }
        public decimal Monto { get; protected set; }
        public decimal Cambio { get; protected set; }
        public string NroDocumento { get; set; }
        public string Referencia { get; set; }

        protected Linea()
        {

        }

        public Linea(string cuentaId, int numero, string descripcion, string monedaId, BalanceTypeEnum balance, decimal cambio, decimal monto)
        {
            CuentaId = Validacion.NotNullOrWhiteSpace(cuentaId, nameof(cuentaId));
            Numero = Validacion.Positive(numero, nameof(numero));
            Descripcion = descripcion;
            MonedaId = Validacion.NotNullOrZero(monedaId, nameof(monedaId));
            Balance = balance;
            Cambio = Validacion.Positive(cambio, nameof(cambio));
            Monto = Validacion.Positive(monto, nameof(monto));
        }
        public override object[] GetKeys()
        {
            return new object[] { AsientoId, Numero };
        }
    }
}
