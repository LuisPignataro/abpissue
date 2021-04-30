using Volo.Abp.Domain.Entities.Auditing;

namespace Programarcr.Contabilidad.Configuracion
{
    public class Moneda: FullAuditedEntity<string>
    {
        protected Moneda()
        {

        }

        public Moneda(string id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public string Nombre { get; set; }
        public decimal Cambio { get; set; }
        public string Simbolo { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }

    }
}
