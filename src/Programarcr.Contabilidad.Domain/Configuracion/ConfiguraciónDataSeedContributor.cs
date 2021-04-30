using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Programarcr.Contabilidad.Configuracion
{
    public class ConfiguraciónDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public IRepository<Moneda, string> _monedaStore;

        public ConfiguraciónDataSeedContributor(IRepository<Moneda, string> monedaStore)
        {
            _monedaStore = monedaStore;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if(await _monedaStore.CountAsync() <= 0)
            {
                await _monedaStore.InsertAsync(new Moneda("CRC", "Colón costarricence") { IsDefault = true, IsEnabled = true });
                await _monedaStore.InsertAsync(new Moneda("USD", "Dólar estadounidense") { IsEnabled = true });
                await _monedaStore.InsertAsync(new Moneda("EUR", "Unidad monetaria europea"));
            }
        }
    }
}
