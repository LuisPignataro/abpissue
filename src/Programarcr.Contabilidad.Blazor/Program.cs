using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

namespace Programarcr.Contabilidad.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string licencia = "Mzc0ODY3QDMxMzgyZTM0MmUzMEFpU0FpbXh0OFlDT3RzOTdFY0tGSVJZa1dPWXV4YWtoaVl6VVdmeE5iLzQ9;Mzc0ODY4QDMxMzgyZTM0MmUzMFBPUGNLcFgvU0duTFNhKzNmUWp0K3orTVBCdEJ2Vk02ejdoSWs2TUpTbU09;Mzc0ODY5QDMxMzgyZTM0MmUzMGRRZitEdlZDOUlLS1ltakV4VW5sdkNlSzJEZFBPcFhlUWZJSmVRQXovVkk9;Mzc0ODcwQDMxMzgyZTM0MmUzMEhkTmc3UmpPMno3bktzZHZnc1RqUjNmZGNPOFE5RXRla3NUeHhIUkhBR2s9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licencia);
            
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var application = builder.AddApplication<ContabilidadBlazorModule>(options =>
            {
                options.UseAutofac();
            });

            builder.Services.AddSyncfusionBlazor();
            
            var host = builder.Build();

            await application.InitializeAsync(host.Services);
            
            await host.RunAsync();
        }
    }
}
