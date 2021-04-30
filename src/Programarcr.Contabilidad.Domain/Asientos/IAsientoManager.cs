using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Programarcr.Contabilidad.Asientos
{
    public interface IAsientoManager : IDomainService
    {
        Task<Asiento> CreateAsync(DateTime fecha);
    }
}
