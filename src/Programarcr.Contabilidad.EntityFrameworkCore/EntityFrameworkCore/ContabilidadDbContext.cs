using Microsoft.EntityFrameworkCore;
using Programarcr.Contabilidad.Asientos;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Periodos;
using Programarcr.Contabilidad.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Programarcr.Contabilidad.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See ContabilidadMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class ContabilidadDbContext : AbpDbContext<ContabilidadDbContext>
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<TipoCambio> TiposDeCambio { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<PeriodoContable> Periodos { get; set; }
        public DbSet<CentroCosto> CentrosDeCosto { get; set; }
        public DbSet<Asiento> Asientos { get; set; }

        public ContabilidadDbContext(DbContextOptions<ContabilidadDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the ContabilidadEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureContabilidad method */

            builder.ConfigureContabilidad();
        }
    }
}
