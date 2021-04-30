using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentaSearchInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public CuentaSearchInput()
        {
            Filter = string.Empty;
        }
    }
}