using System;

namespace Programarcr.Contabilidad.Cuentas
{
    public class NumeroCuentaNoValidaException : Exception
    {
        public NumeroCuentaNoValidaException(string numero, string paramName): base($"El numero de cuenta {numero} no es válido para el parametro {paramName}")
        {
            Numero = numero;
            ParamName = paramName;
        }

        public string Numero { get; set; }
        public string ParamName { get; set; }
    }

    public class NumeroCuentaNotRootException : Exception
    {
        public NumeroCuentaNotRootException(string numero, string paramName) : base($"El numero de cuenta {numero} no es válido para una cuenta root")
        {
            Numero = numero;
            ParamName = paramName;
        }

        public string Numero { get; set; }
        public string ParamName { get; set; }
    }

    public class NumeroCuentaNoHijaException : Exception
    {
        public NumeroCuentaNoHijaException(string numero, string numeroParent) : base($"El numero de cuenta {numeroParent} no es válido como hija de {numero}")
        {
            Numero = numero;
            NumeroParent = numeroParent;
        }

        public string Numero { get; set; }
        public string NumeroParent { get; set; }
    }

    public class CuentaNotFountException : Exception
    {
        public CuentaNotFountException(string numero) : base($"El numero de cuenta {numero} no existe")
        {
            Numero = numero;
        }

        public string Numero { get; set; }
    }

    public class CuentaDuplicadaException : Exception
    {
        public CuentaDuplicadaException(string numero) : base($"El numero de cuenta {numero} ya existe")
        {
            Numero = numero;
        }

        public string Numero { get; set; }
    }
}
