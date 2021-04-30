using Volo.Abp.DependencyInjection;

namespace Programarcr.Contabilidad.Cuentas
{
    public interface ICuentaFormatProvider : ISingletonDependency
    {
        int Levels { get; }

        string AddFormat(string numeroCuenta);
        string GetLevelMask(int level);
        int GetLevelOf(string numeroCuenta);
        int GetLevelWidth(int level);
        string GetParent(string numeroCuenta);
        bool IsValid(string numeroCuenta);
        string RemoveFormat(string numeroCuenta);
        string GetPrefixAccountFromParent(string idAccount);
        string SetFormatedPrefixAccountFromParent(string idAccount);
        string SetFormatedSufixAccountFromParent(string idAccount);
    }
}
