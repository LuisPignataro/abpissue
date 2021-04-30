using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Localization;
using Programarcr.Contabilidad.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlazoriseUI;
using Volo.Abp.Timing;

namespace Programarcr.Contabilidad.Blazor.Pages.Configuracion
{
    public class CotizacionModel : AbpCrudPageBase<ITipoCambioAppService, TipoCambioDto, int, TipoCambioFilterDto, CreateUpdateTipoCambioDto>
    {
        
        [Inject] 
        protected IMonedaAppService monedaAppService { get; set; }

        public List<MonedaDto> Monedas = new List<MonedaDto>();

        public string SelectedMonedaId;
        public MonedaDto DefaultMoneda;


        public CotizacionModel()
        {
            LocalizationResource = typeof(ContabilidadResource);
            CreatePolicyName = ContaPermissions.Cotizacion.CreateEdit;
            UpdatePolicyName = ContaPermissions.Cotizacion.CreateEdit;
            DeletePolicyName = ContaPermissions.Cotizacion.Delete;
        }
        
        protected override async Task OnInitializedAsync()
        {
            // TODO: revisar esta parte para evitar que se carge los datos 2 veces
            await base.OnInitializedAsync();
            
            DefaultMoneda = await monedaAppService.GetDefault();
            Monedas = (await monedaAppService.GetListAsync(new MonedaFilterDto()
            {
                IsEnabled = true,
            })).Items.Where(dto => !dto.IsDefault).ToList();

            SelectedMonedaId = Monedas.FirstOrDefault()?.Id;
            GetListInput.MonedaId = SelectedMonedaId;
            await GetEntitiesAsync();
        }
        
        public async Task OnSelectedValueChanged(string value)
        {
            SelectedMonedaId = value;
            GetListInput.MonedaId = SelectedMonedaId;
            await GetEntitiesAsync();
        }

        protected override async Task OpenCreateModalAsync()
        {
            await base.OpenCreateModalAsync();
            
            NewEntity.MonedaId = SelectedMonedaId;
            NewEntity.Fecha = DateTime.Now;
        }
        
        protected override async Task OnDataGridReadAsync(DataGridReadDataEventArgs<TipoCambioDto> e)
        {
            CurrentSorting = "Fecha DESC";
            CurrentPage = e.Page;
            await GetEntitiesAsync();
        }

    }
}