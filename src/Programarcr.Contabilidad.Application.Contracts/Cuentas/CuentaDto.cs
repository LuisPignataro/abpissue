using System.Collections.ObjectModel;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentaDto: FullAuditedEntityDto<string>
    {
        public string Nombre { get; set; }
        public Collection<CuentaDto> Children { get; set; }
        public bool HasChildren { get; set; }
    }
}
