using System.Collections.Generic;
using Programarcr.Contabilidad.Configuracion.Services;
using Programarcr.Contabilidad.Permissions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Programarcr.Contabilidad.Configuracion
{
    public class MonedaAppService: AbstractKeyReadOnlyAppService<Moneda, MonedaDto, MonedaDto, string, MonedaFilterDto>, IMonedaAppService
    {
        private readonly IRepository<Moneda, string> _monedaStore;
        private readonly IMonedaManager monedaManager;

        public string SetPolicyName { get; set; }  

        public MonedaAppService(IRepository<Moneda, string> monedaStore, IMonedaManager monedaManager): base(monedaStore)
        {
            _monedaStore = monedaStore;
            this.monedaManager = monedaManager;
            SetPolicyName = ContaPermissions.Monedas.CreateEdit;
            GetListPolicyName = ContaPermissions.Monedas.Default;
        }

        public virtual async Task SetDefault(string id)
        {
            await CheckSetPoliciAsync();

            try
            {
                await monedaManager.SetDefault(id);
            }
            catch (EntityNotFoundException) {
                throw new UserFriendlyException($"La moneda {id} no existe");
            }
            catch(MonedaNotEnabledExeption)
            {
                throw new UserFriendlyException("No se puede establecer como por defecto una moneda no habilitada");
            }
        }

        private async Task CheckSetPoliciAsync()
        {
            await CheckPolicyAsync(SetPolicyName);
        }

        public virtual async Task Enable(string id)
        {
            var moneda = await GetEntityByIdAsync(id);
            moneda.IsEnabled = true;
        }

        public virtual async Task Disable(string id)
        {
            var habilitadas = await AsyncExecuter.CountAsync(_monedaStore.Where(x => x.IsEnabled));

            if (habilitadas < 2)
                throw new UserFriendlyException("Debe haber al menos una moneda habilitada");

            var moneda = await GetEntityByIdAsync(id);
            moneda.IsEnabled = false;
        }

        public virtual async Task<MonedaDto> GetDefault()
        {
            return MapToGetOutputDto(await monedaManager.GetDefault());
        }
        protected override IQueryable<Moneda> CreateFilteredQuery(MonedaFilterDto input)
        {
            return base.CreateFilteredQuery(input)
                .Where(x => x.IsEnabled == input.IsEnabled);
        }
        protected override async Task<Moneda> GetEntityByIdAsync(string id)
        {
            Check.NotNullOrWhiteSpace(id, "Moneda");

            try
            {
                return await _monedaStore.GetAsync(id);
            }catch(EntityNotFoundException ex)
            {
                throw new UserFriendlyException("La moneda es inexistente",innerException: ex);
            }

        }
    }
}
