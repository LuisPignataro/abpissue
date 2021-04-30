namespace Programarcr.Contabilidad.Cuentas
{
    public class CreateCuentaDto
    {
        public string ParentId { get; set; }
        public string NumeroCuenta { get; set; }
        public CuentaTypeEnum TipoCuenta { get; set; }
        public string Moneda { get; set; }
        public BalanceTypeEnum TipoBalance { get; set; }
        public string Nombre { get; set; }
        public int Level { get; set; }
        public string Resultado { get; set; }
        public bool IsValid { get; set; }
    }
}