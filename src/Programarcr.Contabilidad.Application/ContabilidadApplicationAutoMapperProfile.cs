using AutoMapper;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Periodos;
using Programarcr.Contabilidad.Periodos.Dtos;
using Programarcr.Contabilidad.ValueObjects;

namespace Programarcr.Contabilidad
{
    public class ContabilidadApplicationAutoMapperProfile : Profile
    {
        public ContabilidadApplicationAutoMapperProfile()
        {
            CreateMap<Moneda, MonedaDto>();
            CreateMap<UpdateMonedaDto, Moneda>();

            CreateMap<Cuenta, CuentaDto>();

            CreateMap<CentroCosto, CentroCostoDto>().ReverseMap();

            CreateMap<PeriodoContable, PeriodoContableDto>()
                .ForMember(dto => dto.Fin, option => option.MapFrom(periodo => periodo.Fin))
                .ForMember(dto => dto.Inicio, option => option.MapFrom(periodo => periodo.Inicio));
            
            CreateMap<MesContable, MesContableEditDto>()
                .ReverseMap();

            // CreateMap<PeriodoContable, PeriodoContableDto>()
            //     .ForMember(dto => dto.MesContableInicioAño, option => option.MapFrom(contable => contable.Inicio.Año))
            //     .ForMember(dto => dto.MesContableInicioMes, option => option.MapFrom(contable => contable.Inicio.Mes))
            //     .ForMember(dto => dto.MesContableFinAño, option => option.MapFrom(contable => contable.Fin.Año))
            //     .ForMember(dto => dto.MesContableFinMes, option => option.MapFrom(contable => contable.Fin.Mes));
            
            CreateMap<TipoCambio, TipoCambioDto>();
            CreateMap<CreateUpdateTipoCambioDto, TipoCambio>();
            CreateMap<CreateUpdateTipoCambioDto, TipoCambioDto>();
        }
    }
}
