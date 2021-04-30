using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Programarcr.Contabilidad.Cuentas
{
    public class CuentaFormatProviderTests: ContabilidadDomainTestBase
    {
        ICuentaFormatProvider provider;
        public CuentaFormatProviderTests()
        {
            provider = GetRequiredService<ICuentaFormatProvider>();
        }

        [Fact]
        public void DefaultOptionsUse7Levels()
        {
            provider.Levels.ShouldBe(7);
        }

        [Fact]
        public void DefaultLevelsWidth()
        {
            provider.GetLevelWidth(0).ShouldBe(4);
            provider.GetLevelWidth(1).ShouldBe(4);
            provider.GetLevelWidth(2).ShouldBe(3);
            provider.GetLevelWidth(3).ShouldBe(3);
            provider.GetLevelWidth(4).ShouldBe(3);
            provider.GetLevelWidth(5).ShouldBe(2);
            provider.GetLevelWidth(6).ShouldBe(2);
        }

        [Fact]
        public void ValidFormatNumbers()
        {
            provider.IsValid("0100-0000-000-000-000-00-00").ShouldBe(true);
            provider.IsValid("0100-0000-000-000-000-00-01").ShouldBe(true);
            provider.IsValid("0100-0000-000-000-000-10-00").ShouldBe(true);
            provider.IsValid("0100-0000-060-400-000-05-03").ShouldBe(true);
            provider.IsValid("0100-0000-000-000-000-00-03").ShouldBe(true);
            provider.IsValid("010000000000000000003").ShouldBe(true);
        }

        [Fact]
        public void InvalidFormatNumbers()
        {
            provider.IsValid("0100-0000-000-000-000-A0-00").ShouldBe(false);
            provider.IsValid("0100-0000-000-000-000-00-1").ShouldBe(false);
            provider.IsValid("0100-0000-000-000-000-10-000").ShouldBe(false);

            provider.IsValid("01000000000000000A000").ShouldBe(false);
            provider.IsValid("01000000000000000001").ShouldBe(false);
            provider.IsValid("0100000000000000010000").ShouldBe(false);

        }

        [Fact]
        public void GetLevelOfLevel0()
        {
            provider.GetLevelOf("0100-0000-000-000-000-00-00").ShouldBe(0);
        }

        [Fact]
        public void GetParentOfCorrect()
        {
            provider.AddFormat(provider.GetParent("0100-0001-001-001-001-01-01")).ShouldBe("0100-0001-001-001-001-01-00");
            provider.AddFormat(provider.GetParent("0100-0001-001-001-001-01-00")).ShouldBe("0100-0001-001-001-001-00-00");
            provider.AddFormat(provider.GetParent("0100-0001-001-001-001-00-00")).ShouldBe("0100-0001-001-001-000-00-00");
            provider.AddFormat(provider.GetParent("0100-0001-001-000-000-00-00")).ShouldBe("0100-0001-000-000-000-00-00");
            provider.AddFormat(provider.GetParent("0100-0001-000-000-000-00-00")).ShouldBe("0100-0000-000-000-000-00-00");
        }

        [Fact]
        public void Error_GetParentOfRoot()
        {
            Assert.Throws<NumeroCuentaNoHijaException>(() => 
            {
                var x = provider.GetParent("0100-0000-000-000-000-00-00");
            });
        }
    }
}
