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
    public class PeriodosContablesManagerTests: ContabilidadDomainTestBase
    {
        private IPeriodoContableManager periodoContableManager;
        public PeriodosContablesManagerTests()
        {
            periodoContableManager = GetRequiredService<IPeriodoContableManager>();
        }

        [Fact]
        public async Task Createfirst()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));
            });
            
        }

        [Fact]
        public async Task CreateSecond()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));
                await periodoContableManager.CreateAsync(new MesContable(2011, 1), new MesContable(2011, 12));
            });

        }

        [Fact]
        public async Task TryCreateSecondWrongMonth()
        {
            
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));

                await Assert.ThrowsAsync<ArgumentException>(async () =>
                {
                    await periodoContableManager.CreateAsync(new MesContable(2010, 12), new MesContable(2011, 12));
                });
                
            });

        }

        [Fact]
        public async Task CreateAndGet()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var p = await periodoContableManager.GetLastAsync();

                p.Codigo.ShouldBe(2010);
            });


        }

        [Fact]
        public async Task CreateAñoCortadoAndGetByDate()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2008, 6), new MesContable(2009, 5));
                await periodoContableManager.CreateAsync(new MesContable(2009, 6), new MesContable(2010, 5));
                await periodoContableManager.CreateAsync(new MesContable(2010, 6), new MesContable(2011, 5));
            });

            await WithUnitOfWorkAsync(async () =>
            {
                var p = await periodoContableManager.GetByDateAsync(new DateTime(2009,12,5));
                var p2 = await periodoContableManager.GetByDateAsync(new DateTime(2010, 2, 5));

                p.Codigo.ShouldBe(2009);
                p2.Codigo.ShouldBe(2009);
            });


        }

        [Fact]
        public async Task CreateAndGetByDate()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                await periodoContableManager.CreateAsync(new MesContable(2008, 1), new MesContable(2008, 12));
                await periodoContableManager.CreateAsync(new MesContable(2009, 1), new MesContable(2009, 12));
                await periodoContableManager.CreateAsync(new MesContable(2010, 1), new MesContable(2010, 12));


            });

            await WithUnitOfWorkAsync(async () =>
            {
                var p = await periodoContableManager.GetByDateAsync(new DateTime(2009, 12, 5));
                var p2 = await periodoContableManager.GetByDateAsync(new DateTime(2010, 2, 5));

                p.Codigo.ShouldBe(2009);
                p2.Codigo.ShouldBe(2010);
            });


        }


    }
}
