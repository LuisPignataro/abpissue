using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Configuracion
{
    public class MonedaDto: EntityDto<string>
    {
        public string Nombre { get; set; }
        public decimal Cambio { get; set; }
        public string Simbolo { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }
    }
}
