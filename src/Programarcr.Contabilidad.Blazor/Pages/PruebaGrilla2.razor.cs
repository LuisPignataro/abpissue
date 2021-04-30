using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Components;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Volo.Abp.Timing;

namespace Programarcr.Contabilidad.Blazor.Pages
{
    public class PruebaGrilla2Model: ContabilidadComponentBase
    {
        #region Members
        [Inject]
        public ICuentasAppService CuentasAppService { get; set; }
        
        [Inject]
        public IMonedaAppService MonedaAppService { get; set; }

        
        public SfAutoComplete<string, SelectCuentaViewModelItem> AutoCompleteRef { get; set; }
        public List<SelectCuentaViewModelItem> CuentaSelectList = new List<SelectCuentaViewModelItem>();
        public IReadOnlyList<MonedaDto> MonedaSelectList { get; set; }
        public AsientoContableViewModel SelectedAsientoContable;
        public DataGrid<AsientoContableViewModel> DataGrid;

        public List<AsientoContableViewModel> Asientos = new List<AsientoContableViewModel>
        {
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
            new AsientoContableViewModel { CuentaId = "010000010010010010300", MonedaId = "01000", Descripcion = "Descripcion", Monto = 45, Movimiento = "Entrada", Referencia = 8, NumeroDocumento = "5446213", TipoCambio = 453, TipoDocumento = "DNI"},
        };
        
        #endregion
        
        #region Types
        
        public class AsientoContableViewModel
        {
            public string TipoDocumento { get; set; }
            public string NumeroDocumento { get; set; }
            
            public string MonedaId { get; set; }
            public decimal TipoCambio { get; set; }
            
            public string Descripcion { get; set; }
            public int Referencia { get; set; }
            
            public double Monto { get; set; }
            public string Movimiento { get; set; }
            
            public string CuentaId { get; set; }
        }
        public class SelectCuentaViewModelItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        
        #endregion
        
        protected override async Task OnInitializedAsync()
        {
            var monedas = await MonedaAppService.GetListAsync(new MonedaFilterDto());
            MonedaSelectList = monedas.Items.Where(dto => dto.IsEnabled && !dto.IsDefault).ToList();
            await base.OnInitializedAsync();
        }
        
        public void DescriptionValidation(ValidatorEventArgs obj)
        {
            var selectValue = (string)obj.Value;
            if (string.IsNullOrEmpty(selectValue))
            {
                obj.Status = ValidationStatus.Error;
            }
        }
        
        public async Task OnFilter(FilteringEventArgs arg)
        {
            arg.PreventDefaultAction = true;
            var select = new List<KeyValuePair<string, string>>();
            
            var searchValue = arg.Text ?? string.Empty;
            var data = (await CuentasAppService.GetListAsync(new CuentaSearchInput()
            {
                Filter = searchValue,
                MaxResultCount = 6,
                SkipCount = 1
            }))
                .Items
                .Select(a => new SelectCuentaViewModelItem()
                {
                    Name = $"{a.Nombre} {a.Id}",
                    Value = a.Id
                });
            
            await AutoCompleteRef.Filter(data, new Query());
        }
    }
}