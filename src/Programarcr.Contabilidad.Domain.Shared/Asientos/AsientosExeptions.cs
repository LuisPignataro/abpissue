using System;
using System.Collections.Generic;
using System.Text;

namespace Programarcr.Contabilidad.Asientos
{
    public class AsientoAplicadoExeption: Exception
    {
        public AsientoAplicadoExeption(): base("No se puede modificar un asiento aplicado")
        {

        }
    }
}
