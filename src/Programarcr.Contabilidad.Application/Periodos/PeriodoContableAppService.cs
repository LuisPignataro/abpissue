using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Programarcr.Contabilidad.Periodos.Dtos;
using Programarcr.Contabilidad.ValueObjects;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Programarcr.Contabilidad.Periodos
{
    public class PeriodoContableAppService : 
        CrudAppService<PeriodoContable, 
            PeriodoContableDto, 
            int, 
            PagedAndSortedResultRequestDto, 
            PeriodoContableCreateInput>,
        IPeriodoContableAppService
    {
        private readonly IPeriodoContableManager _periodoContableManager;

        public PeriodoContableAppService(IRepository<PeriodoContable, int> repository, 
            IPeriodoContableManager periodoContableManager) : base(repository)
        {
            _periodoContableManager = periodoContableManager;
        }

        public override async Task<PeriodoContableDto> CreateAsync(PeriodoContableCreateInput input)
        {
            var result = await _periodoContableManager.CreateAsync(
                inicio: new MesContable(input.Inicio.Año, input.Inicio.Mes),
                fin: new MesContable(input.Fin.Año, input.Fin.Mes));
            
            return ObjectMapper.Map<PeriodoContable,PeriodoContableDto>(result);
        }

        public override Task<PeriodoContableDto> UpdateAsync(int id, PeriodoContableCreateInput input)
        {
            throw new NotImplementedException();
        }
        
        public async Task<PeriodoContableDto> GetLastAsync()
        {
            return ObjectMapper.Map<PeriodoContable, PeriodoContableDto>(await _periodoContableManager.GetLastAsync());
        }
    }
}