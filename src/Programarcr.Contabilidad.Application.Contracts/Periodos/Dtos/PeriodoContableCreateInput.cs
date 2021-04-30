using System.ComponentModel.DataAnnotations;

namespace Programarcr.Contabilidad.Periodos.Dtos
{
    public class PeriodoContableCreateInput
    {
        public MesContableEditDto Inicio { get; set; }
        public MesContableEditDto Fin { get; set; }

        public PeriodoContableCreateInput()
        {
            Inicio = new MesContableEditDto();
            Fin = new MesContableEditDto();
        }
    }
}