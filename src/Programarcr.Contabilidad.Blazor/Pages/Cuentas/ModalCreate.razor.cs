using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Volo.Abp.Application.Dtos;

namespace Programarcr.Contabilidad.Blazor.Pages.Cuentas
{
    public partial class ModalCreateModel : ContabilidadComponentBase
    {
        
        [Inject] 
        protected ICuentasAppService cuentasAppService { get; set; }
        [Inject]
        protected ICuentaFormatProvider cuentaFormatProvider { get; set; }
        [Inject]
        protected IMonedaAppService monedasAppService { get; set; }

        [Parameter]
        public EventCallback<CuentaDto> OnNewAccountCreated { get; set; }

        public PagedResultDto<MonedaDto> Monedas = new PagedResultDto<MonedaDto>();
        public CreateCuentaViewModel NuevaCuentaModel;
        public Modal ModalCreateRef;
        protected Validations FormValidations;
        
        public string NumeroCuentaRegEx = string.Empty;
        public int MaxLengthFormSize = 0;

        public string NumeroCuentaSufix = string.Empty;
        public string NumeroCuentaPrefix = string.Empty;
        
        

        public ModalCreateModel()
        {
            NuevaCuentaModel = new CreateCuentaViewModel();
        }
        

        async Task<CreateCuentaResultDto> CrearCuentaChild()
        {
            var prefixAccount = cuentaFormatProvider.GetPrefixAccountFromParent(NuevaCuentaModel.Parent.Id);
            var numeroNuevaCuenta = $"{prefixAccount}{NuevaCuentaModel.Numero}".PadRight(21, '0');
            
            var result = await cuentasAppService.CrearCuenta(new CreateCuentaDto
            {
                ParentId = NuevaCuentaModel.Parent.Id,
                NumeroCuenta = numeroNuevaCuenta,
                Moneda = NuevaCuentaModel.MonedaId,
                TipoBalance = NuevaCuentaModel.TipoBalance,
                Nombre = NuevaCuentaModel.Nombre,
                Level = -1,
            });
            
            return result;
        }

        public async Task ShowModal(CuentaDto parentCuentaDto)
        {
            
            Monedas = await monedasAppService.GetListAsync(new MonedaFilterDto 
            {
                IsEnabled = true,
            });
            
            NuevaCuentaModel = new CreateCuentaViewModel
            {
                Parent = parentCuentaDto,
            };

            BuildRegExAndMaxLengthForNewAccount(parentCuentaDto.Id);
            NumeroCuentaPrefix = cuentaFormatProvider.SetFormatedPrefixAccountFromParent(parentCuentaDto.Id);
            NumeroCuentaSufix = cuentaFormatProvider.SetFormatedSufixAccountFromParent(parentCuentaDto.Id);
            StateHasChanged();
            ModalCreateRef.Show();
        }
        
        private void BuildRegExAndMaxLengthForNewAccount(string idCuenta)
        {
            var nextLevel = cuentaFormatProvider.GetLevelOf(idCuenta) + 1;
            MaxLengthFormSize = cuentaFormatProvider.GetLevelWidth(nextLevel);
            NumeroCuentaRegEx = @"\d{N}".Replace("N", MaxLengthFormSize.ToString());
        }
        
        protected async Task CreateAccountAsync()
        {
            if (FormValidations.ValidateAll())
            {
                var resultCreate = await CrearCuentaChild();
                if (resultCreate.IsCreated)
                {
                    await OnNewAccountCreated.InvokeAsync(NuevaCuentaModel.Parent);
                }
                else
                {
                    Logger.LogWarning("Error al crea la cuenta");
                    await Message.Error(resultCreate.ErrorMessageResult);
                }

                HideModal();
            }
        }

        protected void HideModal()
        {
            FormValidations.ClearAll();
            ModalCreateRef.Hide();
        }
        
    }
}
