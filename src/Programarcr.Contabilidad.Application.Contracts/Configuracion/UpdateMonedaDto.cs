using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class UpdateMonedaDto: EntityDto<string>
    {
        [Required]
        [StringLength(256)]
        public string Nombre { get; set; }
        
        [Required]
        [Range(0D, double.MaxValue)]
        public decimal Cambio { get; set; }

    }
}
