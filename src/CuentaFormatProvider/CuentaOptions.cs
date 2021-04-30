using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentaOptions
    {
        public CuentaOptions()
        {
            Mask = "####-####-###-###-###-##-##";
        }
        public string Mask { get; set; }
    }
}
