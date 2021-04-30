using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace Programarcr.Contabilidad.ValueObjects
{
    public class MesContable : ValueObject
    {
        private MesContable()
        {

        }

        public MesContable(int año, int mes)
        {
            Año = año;
            Mes = mes;
        }

        public int Mes { get; set; }
        public int Año { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Año;
            yield return Mes;

        }

        public static MesContable operator +(MesContable a, int meses)
        {
            int años = 0;
            var nuevo = new MesContable(a.Año, a.Mes);
            if (meses > 12)
            {
                años = meses / 12;
                meses = meses % 12;
            }

            nuevo.Año += años;
            if (nuevo.Mes + meses > 12)
            {
                nuevo.Mes += meses - 12;
                nuevo.Año++;
            }
            else
            {
                nuevo.Mes += meses;
            }


            return nuevo;
        }

        public static MesContable operator ++(MesContable a) => a += 1;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (MesContable)obj;

            return Año == other.Año && Mes == other.Mes;
        }
    }
}