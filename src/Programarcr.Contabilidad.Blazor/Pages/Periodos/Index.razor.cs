using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Localization;
using Programarcr.Contabilidad.Periodos;
using Programarcr.Contabilidad.Periodos.Dtos;
using Programarcr.Contabilidad.ValueObjects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlazoriseUI;

namespace Programarcr.Contabilidad.Blazor.Pages.Periodos
{
    public class IndexModel : AbpCrudPageBase<IPeriodoContableAppService,
    PeriodoContableDto,int,PagedAndSortedResultRequestDto, PeriodoContableCreateInput>
    {
        public IndexModel():base()
        {
            LocalizationResource = typeof(ContabilidadResource);
            NewEntity = new PeriodoContableCreateInput();
        }

        public PeriodoContableDto lastPeriodoContable;
        public bool InicioDisabled = false;

        protected override async Task OpenCreateModalAsync()
        {
            lastPeriodoContable = await AppService.GetLastAsync();
            MesContable inicio = new MesContable(DateTime.Now.Year, DateTime.Now.Month);

            if (lastPeriodoContable != null)
            {
                inicio = lastPeriodoContable.Fin + 1;
                InicioDisabled = true;
            }

            await base.OpenCreateModalAsync();
            NewEntity.Inicio = ObjectMapper.Map<MesContable, MesContableEditDto>(inicio);
            NewEntity.Fin = ObjectMapper.Map<MesContable, MesContableEditDto>(inicio+11);

            
        }

        protected override Task OnCreatedEntityAsync()
        {
            return base.GetEntitiesAsync();
        }
        public void ValidacionInicio(ValidatorEventArgs validationEventArgs)
        {
            var mesContable = (MesContableEditDto)validationEventArgs.Value;

            if (mesContable.Año == 0 || mesContable.Año < 1900)
            {
                validationEventArgs.Status = ValidationStatus.Error;
            }

            if (lastPeriodoContable != null)
            {
                if (mesContable.Año >= lastPeriodoContable.Fin.Año && mesContable.Mes >= lastPeriodoContable.Fin.Mes)
                {
                    validationEventArgs.Status = ValidationStatus.Success;
                }
                else
                {
                    validationEventArgs.Status = ValidationStatus.Error;
                }
            }
        }

        public void ValidacionFin(ValidatorEventArgs validationEventArgs)
        {
            var mesContable = (MesContableEditDto)validationEventArgs.Value;

            if (mesContable.Año == 0 || mesContable.Año < 1900)
            {
                validationEventArgs.Status = ValidationStatus.Error;
            }

            if (mesContable.Mes > this.NewEntity.Inicio.Mes && mesContable.Año >= this.NewEntity.Inicio.Año ||
                mesContable.Mes == this.NewEntity.Inicio.Mes && mesContable.Año > this.NewEntity.Inicio.Año)
            {
                validationEventArgs.Status = ValidationStatus.Success;
            }
            else
            {
                validationEventArgs.Status = ValidationStatus.Error;
            }
        }
        
    }
}