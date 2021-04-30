using Programarcr.Contabilidad.Cuentas;
using Programarcr.Contabilidad.Periodos;
using Programarcr.Contabilidad.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Programarcr.Contabilidad.Asientos
{
    public class AsientosTests: ContabilidadDomainTestBase
    {
        IAsientoManager asientoManager;
        private IPeriodoContableManager periodoContableManager;
        ICuentasManager cuentasManager;

        public AsientosTests()
        {
            asientoManager = GetRequiredService<IAsientoManager>();
            periodoContableManager = GetRequiredService<IPeriodoContableManager>();
            cuentasManager = GetRequiredService<ICuentasManager>();

        }

        [Fact]
        public async Task CreateTest()
        {
            await WithUnitOfWorkAsync(async () =>
            {

                var cuenta = await cuentasManager.CreateCuentaRootAsync(
                    numero: "0100-0000-000-000-000-00-00",
                    tipo: CuentaTypeEnum.Activo,
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo");

                var child1 = await cuentasManager.CreateCuentaChildAsync(
                    parent: cuenta.Id,
                    numero: "0100-0001-000-000-000-00-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo");

                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));

            });

            Asiento asiento = null;

            await WithUnitOfWorkAsync(async () =>
            {
                asiento = await asientoManager.CreateAsync(new DateTime(2010,1,2));
                asiento.AddLinea(
                    cuentaId: "010000010000000000000", 
                    descripcion: "Asiento de prueba",
                    balance: BalanceTypeEnum.Debito, 
                    monedaId: "CRC",
                    cambio: 1, 
                    monto: 150.20M
                    );
            });

            asiento.Lineas.Count.ShouldBe(1);
        }
    }
}
