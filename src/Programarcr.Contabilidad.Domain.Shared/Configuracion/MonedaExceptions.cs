using System;
using System.Collections.Generic;
using System.Text;

namespace Programarcr.Contabilidad.Configuracion
{
    public class MonedaNotEnabledExeption: Exception
    {
        public MonedaNotEnabledExeption(string id): base($"La moneda {id} no está habilitada")
        {

        }
    }

    public class MonedaNotFountExeption : Exception
    {
        public MonedaNotFountExeption(string id) : base($"La moneda {id} no existe")
        {

        }
    }

}
