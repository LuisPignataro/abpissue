using Programarcr.Contabilidad.Configuracion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentasManager : DomainService, ICuentasManager
    {
        #region Private Fields

        private IRepository<Cuenta, string> cuentaStore;
        private ICuentaFormatProvider formatProvider;
        private IMonedaManager monedaManager;

        #endregion Private Fields

        #region Public Constructors

        public CuentasManager(IRepository<Cuenta, string> cuentaStore, ICuentaFormatProvider formatProvider, IMonedaManager monedaManager)
        {
            this.cuentaStore = cuentaStore;
            this.formatProvider = formatProvider;
            this.monedaManager = monedaManager;
        }

        #endregion Public Constructors

        #region Public Methods

        public virtual async Task<Cuenta> CreateCuentaChildAsync(string parent, string numero, string monedaId, BalanceTypeEnum balance, string nombre)
        {
            if (await cuentaStore.AnyAsync(x => x.Id == numero))
                throw new CuentaDuplicadaException(numero);

            var cuentaParent = await cuentaStore.GetAsync(parent);

            if (cuentaParent == null)
                throw new CuentaNotFountException(parent);
            var moneda = await monedaManager.GetAsync(monedaId);

            return cuentaParent.AddChild( numero, moneda, balance, nombre, formatProvider);
        }

        public async Task<Cuenta> CreateCuentaRootAsync(string numero, CuentaTypeEnum tipo, string monedaId, BalanceTypeEnum balance, string nombre)
        {
            if (formatProvider.GetLevelOf(numero) != 0)
                throw new NumeroCuentaNotRootException(numero, nameof(numero));

            return await CreateCuentaAsync(numero, tipo, monedaId, balance, nombre);
        }

        public async Task<Cuenta> GetByNumeroAsync(string numero)
        {
            return await cuentaStore.GetAsync(numero);
        }

        public async Task<IList<Cuenta>> GetCuetasRootAsync()
        {
            var store = await cuentaStore.GetQueryableAsync();
            return await store.Where(x => x.Level == 0).ToListAsync();
        }

        public async Task<ICollection<Cuenta>> GetCuetasChildAsync(string id)
        {
            var store = await cuentaStore.GetQueryableAsync();
            var cuenta = await store.Include(x => x.Children).FirstOrDefaultAsync(x=> x.Id == id);

            if (cuenta == null)
                throw new CuentaNotFountException(id);

            return cuenta.Children;
        }

        public bool ExistAnyCuenta()
        {
            return cuentaStore.Any();
        }
        #endregion Public Methods

        #region Private Methods

        private string CheckNumero(string numero, string paramName)
        {
            if (!formatProvider.IsValid(numero))
                throw new NumeroCuentaNoValidaException(numero, paramName);

            return formatProvider.RemoveFormat(numero);
        }

        private async Task<Cuenta> CreateCuentaAsync(string numero, CuentaTypeEnum tipo, string monedaId, BalanceTypeEnum balance, string nombre)
        {
            numero = CheckNumero(numero, nameof(numero));

            if (await cuentaStore.AnyAsync(x => x.Id == numero))
                throw new CuentaDuplicadaException(numero);

            var moneda = await monedaManager.GetAsync(monedaId);

            var cuenta = new Cuenta(numero, null, tipo, moneda, balance)
            {
                Nombre = nombre
            };

            await cuentaStore.InsertAsync(cuenta, autoSave: true);

            return cuenta;
        }

        #endregion Private Methods
    }
}