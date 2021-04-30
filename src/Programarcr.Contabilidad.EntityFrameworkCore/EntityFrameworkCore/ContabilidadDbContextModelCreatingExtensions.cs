using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Programarcr.Contabilidad.Asientos;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Periodos;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Programarcr.Contabilidad.EntityFrameworkCore
{
    public static class ContabilidadDbContextModelCreatingExtensions
    {

        public static void ConfigureContabilidad(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            ConfigureConfiguracion(builder);
            ConfigureCuentas(builder);
            ConfigurePeriodos(builder);
            ConfigureAsientos(builder);
        }

        private static void ConfigureAsientos(ModelBuilder builder)
        {
            builder.Entity<Asiento>(b =>
            {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
                b.HasMany(x => x.Lineas).WithOne().HasForeignKey(x => x.AsientoId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Linea>(b =>
            {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
            });
        }

        private static void ConfigurePeriodos(ModelBuilder builder)
        {
            builder.Entity<PeriodoContable>(b =>
            {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
                b.OwnsOne(x => x.Inicio);
                b.OwnsOne(x => x.Fin);
            });
        }

        private static void ConfigureCuentas(ModelBuilder builder)
        {
            builder.Entity<Cuenta>(b => {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
                b.Property(x => x.Id).HasMaxLength(21);
                b.HasOne(x=> x.Parent).WithMany(x=> x.Children).HasForeignKey(x=> x.ParentId).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull);
            });
        }

        private static void ConfigureConfiguracion(ModelBuilder builder)
        {
            builder.Entity<Moneda>(b => {
                b.ToContabilidadTable();
                b.Property(x => x.Id).HasMaxLength(3);
                b.ConfigureByConvention();
            });

            builder.Entity<TipoCambio>(b =>
            {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
                b.Property(x => x.MonedaId).HasMaxLength(3);
                b.HasIndex(i => new { i.MonedaId, i.Fecha }).HasDatabaseName("MonedaFechaIndex").IsUnique();
                b.Property(x => x.Compra).HasPrecision(12, 4);
                b.Property(x => x.Venta).HasPrecision(12, 4);
            });

            builder.Entity<CentroCosto>(b =>
            {
                b.ToContabilidadTable();
                b.ConfigureByConvention();
                b.Property(x => x.Nombre).HasMaxLength(255);
                b.HasIndex(x => x.Nombre).IsUnique().HasDatabaseName("IX_Nombre");
            });
        }

        public static EntityTypeBuilder ToContabilidadTable([NotNull] this EntityTypeBuilder entityTypeBuilder, string name = "") {

            if (string.IsNullOrEmpty(name))
            {
                name = entityTypeBuilder.Metadata.GetDefaultTableName();
            }
            entityTypeBuilder.ToTable($"{ContabilidadConsts.DbTablePrefix}{name}" , ContabilidadConsts.DbSchema);

            return entityTypeBuilder;
        }
    }
}