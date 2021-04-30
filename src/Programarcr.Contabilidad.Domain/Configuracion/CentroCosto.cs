using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Programarcr.Contabilidad.Configuracion
{
    public class CentroCosto: Entity<int>
    {
        public string Nombre { get; set; }
    }
}
