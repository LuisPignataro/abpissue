using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.CompilerServices;
using Programarcr.Contabilidad.Data;
using Serilog;
using Volo.Abp;

namespace Programarcr.Contabilidad.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        bool migrarDatos = false;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<ContabilidadDbMigratorModule>(options =>
            {
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();


                await application
                    .ServiceProvider
                    .GetRequiredService<ContabilidadDbMigrationService>()
                    .MigrateAsync();


                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
