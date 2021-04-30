using System;
using System.ComponentModel.DataAnnotations;

namespace Programarcr.Contabilidad.Configuracion
{
    public class CreateUpdateTipoCambioDto
    {
        [Required]
        public string MonedaId { get; set; }
        
        [Required]
        public DateTime Fecha { get; set; }
        
        [Range(0d, Double.MaxValue)]
        [Required]
        public decimal Compra { get; set; }
        
        [Range(0d, Double.MaxValue)]
        [Required]
        public decimal Venta { get; set; }
    }
}