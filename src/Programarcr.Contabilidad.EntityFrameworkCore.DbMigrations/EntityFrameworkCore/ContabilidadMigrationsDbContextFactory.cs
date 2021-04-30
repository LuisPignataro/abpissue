using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Programarcr.Contabilidad.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class ContabilidadMigrationsDbContextFactory : IDesignTimeDbContextFactory<ContabilidadMigrationsDbContext>
    {
        public ContabilidadMigrationsDbContext CreateDbContext(string[] args)
        {
            ContabilidadEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<ContabilidadMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new ContabilidadMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Programarcr.Contabilidad.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
