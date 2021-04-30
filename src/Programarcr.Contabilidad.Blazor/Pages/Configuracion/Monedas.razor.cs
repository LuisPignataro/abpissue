using Blazorise;
using Microsoft.AspNetCore.Authorization;
using Programarcr.Contabilidad.Blazor.Base;
using Programarcr.Contabilidad.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Blazor.Pages.Configuracion
{
    public partial class MonedasBase: ListPageBase<IMonedaAppService, MonedaDto, MonedaDto, string, MonedaFilterDto, MonedaDto>
    {
        protected Validations CreateValidationsRef;
        protected Modal CreateModal;
        protected MonedaDto EditEntity;
        protected IEnumerable<MonedaDto> DisabledMonedas;

        protected string CreatePolicyName { get; set; }
        protected string UpdatePolicyName { get; set; }
        protected string DeletePolicyName { get; set; }

        public bool HasCreatePermission { get; set; }
        public bool HasUpdatePermission { get; set; }
        public bool HasDeletePermission { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await SetPermissionsAsync();
        }

        private async Task SetPermissionsAsync()
        {
            if (CreatePolicyName != null)
            {
                HasCreatePermission = await AuthorizationService.IsGrantedAsync(CreatePolicyName);
            }

            if (UpdatePolicyName != null)
            {
                HasUpdatePermission = await AuthorizationService.IsGrantedAsync(UpdatePolicyName);
            }

            if (DeletePolicyName != null)
            {
                HasDeletePermission = await AuthorizationService.IsGrantedAsync(DeletePolicyName);
            }
        }

        protected virtual async Task OpenCreateModalAsync()
        {
            CreateValidationsRef?.ClearAll();

            await CheckCreatePolicyAsync();
            
            EditEntity = new MonedaDto();

            //TODO: Permitir busqueda online
            var r = await AppService.GetListAsync(new MonedaFilterDto { IsEnabled = false, MaxResultCount = 1000 });
            DisabledMonedas = r.Items;

            // Mapper will not notify Blazor that binded values are changed
            // so we need to notify it manually by calling StateHasChanged
            await InvokeAsync(() => StateHasChanged());

            CreateModal.Show();
        }

        protected virtual async Task CreateEntityAsync()
        {
            await AppService.Enable(EditEntity.Id);
        }

        protected virtual Task CloseCreateModalAsync()
        {
            CreateModal.Hide();
            return Task.CompletedTask;
        }

        protected virtual async Task CheckCreatePolicyAsync()
        {
            await CheckPolicyAsync(CreatePolicyName);
        }

        protected void AutoCompleteChanged(object newValue)
        {
            EditEntity = newValue as MonedaDto;
        }
    }
}
