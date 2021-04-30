namespace Programarcr.Contabilidad.Cuentas
{
    public class CreateCuentaResultDto
    {
        public string NumeroCuenta { get; set; }
        public string ErrorMessageResult { get; set; }
        public bool IsCreated { get; set; }
    }
}