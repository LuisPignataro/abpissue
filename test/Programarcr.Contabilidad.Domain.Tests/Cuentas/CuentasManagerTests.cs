using Programarcr.Contabilidad.Configuracion.Services;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentasManagerTests: ContabilidadDomainTestBase
    {
        ICuentasManager cuentasManager;

        public CuentasManagerTests()
        {
            cuentasManager = GetRequiredService<ICuentasManager>();
        }

        [Fact]
        public async Task CanCreateCuentaRoot()
        {

            Cuenta cuenta = null;
            await WithUnitOfWorkAsync(async () =>
            {
                cuenta = await cuentasManager.CreateCuentaRootAsync(
                    numero: "0100-0000-000-000-000-00-00",
                    tipo: CuentaTypeEnum.Activo,
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo");
            });

            cuenta.ShouldNotBe(null);
            cuenta.Id.ShouldBe("010000000000000000000");
        }

        [Fact]
        public async Task ExceptionCreateCuentaRoot_Whith_Invalid_Numero()
        {

            Cuenta cuenta = null;

            var exception = await Assert.ThrowsAsync<NumeroCuentaNotRootException>(async () =>
            {
                await WithUnitOfWorkAsync(async () =>
                {
                    cuenta = await cuentasManager.CreateCuentaRootAsync(
                        numero: "0100-0001-000-000-000-00-00",
                        tipo: CuentaTypeEnum.Activo,
                        monedaId: "CRC",
                        balance: BalanceTypeEnum.Debito,
                        nombre: "Activo");
                });
            });

            exception.Message.ShouldContain("numero");
        }

        [Fact]
        public async Task CanCreateCuentaChild()
        {
            Cuenta cuenta = null;
            Cuenta child1 = null, child2 = null, child3 = null, child4 = null, child5 = null, child6 = null;

            await WithUnitOfWorkAsync(async () =>
            {

                cuenta = await cuentasManager.CreateCuentaRootAsync(
                    numero: "0100-0000-000-000-000-00-00",
                    tipo: CuentaTypeEnum.Activo,
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo");
            });

            cuenta.ShouldNotBe(null);
            cuenta.Id.ShouldBe("010000000000000000000");

            await WithUnitOfWorkAsync(async () =>
            {
                child1 = await cuentasManager.CreateCuentaChildAsync(
                    parent: cuenta.Id,
                    numero: "0100-0001-000-000-000-00-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo");
            });

            await WithUnitOfWorkAsync(async () =>
            {
                child2 = await cuentasManager.CreateCuentaChildAsync(
                    parent: child1.Id,
                    numero: "0100-0001-001-000-000-00-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo 2");
            });

            await WithUnitOfWorkAsync(async () =>
            {
                child3 = await cuentasManager.CreateCuentaChildAsync(
                    parent: child2.Id,
                    numero: "0100-0001-001-001-000-00-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo 3");
            });

            await WithUnitOfWorkAsync(async () =>
            {
                child4 = await cuentasManager.CreateCuentaChildAsync(
                    parent: child3.Id,
                    numero: "0100-0001-001-001-001-00-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo 4");
            });

            await WithUnitOfWorkAsync(async () =>
            {
                child5 = await cuentasManager.CreateCuentaChildAsync(
                    parent: child4.Id,
                    numero: "0100-0001-001-001-001-01-00",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo 5");
            });

            await WithUnitOfWorkAsync(async () =>
            {
                child6 = await cuentasManager.CreateCuentaChildAsync(
                    parent: child5.Id,
                    numero: "0100-0001-001-001-001-01-01",
                    monedaId: "CRC",
                    balance: BalanceTypeEnum.Debito,
                    nombre: "Activo Fijo 6");
            });

            child6.Id.ShouldBe("010000010010010010101");
            child6.ParentId.ShouldBe("010000010010010010100");
        }

    }
}
