using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Programarcr.Contabilidad.Configuracion
{
    public class MonedaTests: ContabilidadApplicationTestBase
    {
        private MonedaAppService _monedaAppService;
        private IRepository<Moneda, string> _monedaStore;
        private const string MONEDATEST = "TES";

        public MonedaTests()
        {
            _monedaStore = GetRequiredService<IRepository<Moneda, string>>();
            _monedaAppService = GetRequiredService<MonedaAppService>();
        }

        private async Task AgregarMonedaTest()
        {
            await _monedaStore.InsertAsync(new Moneda(MONEDATEST, "Moneda de testing"), autoSave: true);
        }

        [Fact]
        public async Task Should_Get_List_Of_Enabled()
        {
            var list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
            list.TotalCount.ShouldBe(2);
            list.Items.ShouldContain(x => x.Id == "CRC");
        }

        [Fact]
        public async Task Should_Set_Default()
        {
            await _monedaAppService.Enable("USD");
            await _monedaAppService.SetDefault("USD");
            var moneda = await _monedaAppService.GetDefault();

            moneda.Id.ShouldBe("USD");
        }

        [Fact]
        public async Task Error_Seting_Default_Disabled_Moneda()
        {
            var ex = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _monedaAppService.SetDefault("EUR");
            });
        }

        [Fact]
        public async Task Should_Set_Disable()
        {
            await _monedaAppService.Enable("USD");

            var list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
            list.Items.ShouldContain(x => x.Id == "USD");

            await _monedaAppService.Disable("USD");

            list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
            list.Items.ShouldNotContain(x => x.Id == "USD");
        }

        [Fact]
        public async Task Should_Set_Enable()
        {
            await AgregarMonedaTest();

            var list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
            list.Items.ShouldNotContain(x => x.Id == MONEDATEST);

            await _monedaAppService.Enable(MONEDATEST);

            list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
            list.Items.ShouldContain(x => x.Id == MONEDATEST);
        }

        [Fact]
        public async Task Error_When_Set_All_To_Disabled()
        {

            var exception = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                var list = await _monedaAppService.GetListAsync(new MonedaFilterDto { MaxResultCount = 10 });
                foreach (var item in list.Items)
                {
                    await _monedaAppService.Disable(item.Id);
                }
            });

            exception.Message.ShouldContain("al menos una");
        }
    
        [Fact]
        public async Task Error_When_Set_NonExistent()
        {
            var ex = await Assert.ThrowsAsync<UserFriendlyException>(async () =>
            {
                await _monedaAppService.SetDefault("XXX");
            });
        }
    }
}
