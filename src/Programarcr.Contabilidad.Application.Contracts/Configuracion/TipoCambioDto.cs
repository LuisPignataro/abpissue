using System;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class TipoCambioDto : FullAuditedEntityDto<int>
    {
        public string MonedaId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
    }
}