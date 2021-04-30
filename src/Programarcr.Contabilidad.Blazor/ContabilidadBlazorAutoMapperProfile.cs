using AutoMapper;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Periodos.Dtos;
using Programarcr.Contabilidad.ValueObjects;

namespace Programarcr.Contabilidad.Blazor
{
    public class ContabilidadBlazorAutoMapperProfile : Profile
    {
        public ContabilidadBlazorAutoMapperProfile()
        {
            //Define your AutoMapper configuration here for the Blazor project.
            CreateMap<MesContable, MesContableEditDto>();
            CreateMap<PeriodoContableDto, PeriodoContableCreateInput>();
            CreateMap<TipoCambioDto, CreateUpdateTipoCambioDto>()
                .ReverseMap();
        }
    }
}
