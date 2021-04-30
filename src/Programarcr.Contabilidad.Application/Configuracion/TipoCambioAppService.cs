using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Programarcr.Contabilidad.Base;
using Programarcr.Contabilidad.Configuracion.Services;
using Programarcr.Contabilidad.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Programarcr.Contabilidad.Configuracion
{
    
    [Authorize(ContaPermissions.Cotizacion.Default)]
    public class TipoCambioAppService : CrudAppService<TipoCambio, 
        TipoCambioDto, 
        int, 
        TipoCambioFilterDto, 
        CreateUpdateTipoCambioDto>, 
        ITipoCambioAppService
    {
        private readonly ITCManager _tcManager;

        public TipoCambioAppService(IRepository<TipoCambio, int> repository, ITCManager tcManager) : base(repository)
        {
            _tcManager = tcManager;
            GetListPolicyName = ContaPermissions.Cotizacion.Default;
            GetPolicyName = ContaPermissions.Cotizacion.Default;
            
            CreatePolicyName = ContaPermissions.Cotizacion.CreateEdit;
            UpdatePolicyName = ContaPermissions.Cotizacion.CreateEdit;
            DeletePolicyName = ContaPermissions.Cotizacion.Delete;
        }

        protected override async Task<IQueryable<TipoCambio>> CreateFilteredQueryAsync(TipoCambioFilterDto input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(!input.MonedaId.IsNullOrWhiteSpace(), cambio => cambio.MonedaId == input.MonedaId);
        }
        
        public override async Task<TipoCambioDto> CreateAsync(CreateUpdateTipoCambioDto input)
        {
            await CheckCreatePolicyAsync();

            await _tcManager.PushAsync(monedaId: input.MonedaId,
                compra: input.Compra,
                venta: input.Venta,
                fecha: input.Fecha);
            
            // TODO: cuando la compra y venta es igual, no se crea el tipo de cambio, por tanto no se retorna ningun mensaje de error
            
            return ObjectMapper.Map<CreateUpdateTipoCambioDto, TipoCambioDto>(input);
        }
    }
}















