using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Programarcr.Contabilidad.Configuracion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Uow;

namespace Programarcr.Contabilidad.BackGroundWorkers
{
    public class MonedaUpdateWorker : AsyncPeriodicBackgroundWorkerBase
    {
        
        public MonedaUpdateWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory):base(timer, serviceScopeFactory)
        {
            Timer.Period = 3600000; //1 hora
        }
        protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
        {
            Logger.LogInformation("Iniciando: Actualizacion de cotizacion de colones");

            await Task.CompletedTask;

            Logger.LogInformation("Finalizado: Actualizacion de cotizacion de colones");
        }
    }

}
