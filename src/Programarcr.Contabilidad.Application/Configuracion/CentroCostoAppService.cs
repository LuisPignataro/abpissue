using Programarcr.Contabilidad.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Programarcr.Contabilidad.Configuracion
{
    public class CentroCostoAppService: Volo.Abp.Application.Services.CrudAppService<CentroCosto, CentroCostoDto, int>, ICentroCostoAppService
    {
        public CentroCostoAppService(IRepository<CentroCosto, int> repository): base(repository)
        {
            GetListPolicyName = ContaPermissions.CentroDeCostos.Default;
            GetPolicyName = ContaPermissions.CentroDeCostos.Default;
            CreatePolicyName = ContaPermissions.CentroDeCostos.CreateEdit;
            UpdatePolicyName = ContaPermissions.CentroDeCostos.CreateEdit;
            DeletePolicyName = ContaPermissions.CentroDeCostos.CreateEdit;
        }
    }
}
