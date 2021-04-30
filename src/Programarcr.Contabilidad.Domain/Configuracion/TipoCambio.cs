using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Programarcr.Contabilidad.Configuracion
{
    public class TipoCambio: FullAuditedEntity<int>
    {
        public string MonedaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }
}
