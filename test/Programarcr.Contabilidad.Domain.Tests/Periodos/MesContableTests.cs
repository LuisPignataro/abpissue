using Programarcr.Contabilidad.ValueObjects;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Programarcr.Contabilidad.Periodos
{
    public class MesContableTests: ContabilidadDomainTestBase
    {
        [Fact]
        public void suma()
        {
            var mes = new MesContable(2010, 1);

            var uno = mes;
            uno++;

            var diez = mes + 10;
            var once = mes + 12;
            var dosaños = mes + 24;

            uno.Año.ShouldBe(2010);
            uno.Mes.ShouldBe(2);

            diez.Año.ShouldBe(2010);
            diez.Mes.ShouldBe(11);

            once.Año.ShouldBe(2011);
            once.Mes.ShouldBe(1);

            dosaños.Año.ShouldBe(2012);
            dosaños.Mes.ShouldBe(1);
        }
    }
}
