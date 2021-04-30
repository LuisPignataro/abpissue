using Microsoft.AspNetCore.Authorization;
using Programarcr.Contabilidad.Base;
using Programarcr.Contabilidad.Permissions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programarcr.Contabilidad.Configuracion.Services;
using Programarcr.Contabilidad.Periodos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentasAppService: BaseAppService, ICuentasAppService
    {
        private ICuentasManager cuentasManager;
        private readonly IRepository<Cuenta> cuentasRepository;
        private readonly ICuentaFormatProvider _cuentaFormatProvider;
        private readonly IMonedaManager monedaManager;

        public CuentasAppService(ICuentasManager cuentasManager,
            IRepository<Cuenta> cuentasRepository,
            ICuentaFormatProvider cuentaFormatProvider,
            IMonedaManager monedaManager)
        {
            this.cuentasManager = cuentasManager;
            this.cuentasRepository = cuentasRepository;
            _cuentaFormatProvider = cuentaFormatProvider;
            this.monedaManager = monedaManager;
        }

        public async Task<List<CuentaDto>> GetRootListAsync()
        {
            await AuthorizationService.CheckAsync(ContaPermissions.Account.Default);

            var cuentasRoot = await cuentasManager.GetCuetasRootAsync();
            return ObjectMapper.Map<IList<Cuenta>, List<CuentaDto>>(cuentasRoot);
        }

        public async Task<Collection<CuentaDto>> GetChildListAsync(string id)
        {
            await AuthorizationService.CheckAsync(ContaPermissions.Account.Default);

            var cuentas = await cuentasManager.GetCuetasChildAsync(id);
            return ObjectMapper.Map<ICollection<Cuenta>, Collection<CuentaDto>>(cuentas);
        }

        public async Task<CreateCuentaResultDto> CrearCuenta(CreateCuentaDto input)
        {
            try
            {
                if (input.Level == 0)
                {
                    await cuentasManager.CreateCuentaRootAsync(
                        numero: input.NumeroCuenta,
                        tipo: input.TipoCuenta,
                        monedaId: input.Moneda,
                        balance: input.TipoBalance,
                        nombre: input.Nombre
                        );
                }
                else
                {
                    await cuentasManager.CreateCuentaChildAsync(
                        parent: input.ParentId,
                        numero: input.NumeroCuenta,
                        monedaId: input.Moneda,
                        balance: input.TipoBalance,
                        nombre: input.Nombre);
                }

                return new CreateCuentaResultDto()
                {
                    NumeroCuenta = input.NumeroCuenta,
                    IsCreated = true
                };
            }
            catch (Exception exception)
            {
                return new CreateCuentaResultDto()
                {
                    IsCreated = false,
                    NumeroCuenta = input.NumeroCuenta,
                    ErrorMessageResult = exception.Message,
                };
            }

        }

        public async Task<List<CreateCuentaResultDto>> CrearCuentas(List<CreateCuentaDto> input)
        {
            var result = new List<CreateCuentaResultDto>();
            foreach (var item in input)
            {
                result.Add(await CrearCuenta(item));
            }

            return result;
        }

        public bool AlreadyExistsAccount(string numero)
        {
            return cuentasRepository.Any(a => a.Id == numero);
        }

        public async Task<PagedResultDto<CuentaDto>> GetListAsync(CuentaSearchInput input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Cuenta.Id);
            }

            var searchTerm = _cuentaFormatProvider.RemoveFormat(input.Filter);
            
            var repository = await cuentasRepository.GetQueryableAsync();
            
            var result = await repository.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), 
                    cuenta => cuenta.Id.Contains(searchTerm) || 
                              cuenta.Nombre.Contains(searchTerm))
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToListAsync();

            return new PagedResultDto<CuentaDto>(result.Count, ObjectMapper.Map<List<Cuenta>, List<CuentaDto>>(result));
        }
        
    }
}
