using System;
using System.ComponentModel.DataAnnotations;

namespace Programarcr.Contabilidad.Periodos.Dtos
{
    public class MesContableEditDto
    {
        #region Private Fields

        private int año;

        private int mes;

        #endregion Private Fields

        #region Public Events

        public event Action OnChange;

        #endregion Public Events

        #region Public Properties

        [Range(1, Int32.MaxValue)]
        [Required]
        public int Año
        {
            get => año;
            set
            {
                año = value;
                OnChange?.Invoke();
            }
        }

        [Range(1, 12)]
        [Required]
        public int Mes
        {
            get => mes;
            set
            {
                mes = value;
                OnChange?.Invoke();
            }
        }

        #endregion Public Properties
    }
}