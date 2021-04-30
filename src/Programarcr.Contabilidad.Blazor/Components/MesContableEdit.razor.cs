using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Programarcr.Contabilidad.Periodos.Dtos;
using System.Threading.Tasks;

namespace Programarcr.Contabilidad.Blazor.Components
{
    public class MesContableEditModel : BaseInputComponent<MesContableEditDto>
    {
        #region Private Fields

        private bool exitEvent = false;

        #endregion Private Fields

        #region Public Constructors

        public MesContableEditModel()
        {
            InternalValue = new MesContableEditDto();
        }

        #endregion Public Constructors

        #region Public Properties

        [Parameter]
        public MesContableEditDto Value
        {
            get
            {
                return InternalValue;
            }
            set
            {
                if (InternalValue != null)
                {
                    InternalValue.OnChange -= InternalValue_OnChange;
                }

                if (value != null)
                {
                    InternalValue = value;
                    InternalAño = value.Año.ToString();
                    InternalMes = value.Mes;
                    InternalValue.OnChange += InternalValue_OnChange;
                }
            }
        }

        [Parameter]
        public EventCallback<MesContableEditDto> ValueChanged { get; set; }
        public Validation InternalValidation { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected string InternalAño
        {
            get
            {
                if (InternalValue != null)
                    return InternalValue.Año.ToString();
                else
                    return string.Empty;
            }
            set
            {
                bool change = false;
                if (InternalValue != null)
                {
                    if (int.TryParse(value, out int valor))
                    {
                        change = InternalValue.Año != valor;
                        InternalValue.Año = valor;
                    }
                    else
                    {
                        change = InternalValue.Año != 0;
                        InternalValue.Año = 0;
                    }

                    if (change)
                        OnInternalValueChanged(InternalValue);
                }
            }
        }

        protected int InternalMes
        {
            get
            {
                if (InternalValue != null)
                    return InternalValue.Mes;
                else
                    return 1;
            }
            set
            {
                bool change = false;

                if (InternalValue != null)
                {
                    change = InternalValue.Mes != value;
                    InternalValue.Mes = value;

                    if(change)
                        OnInternalValueChanged(InternalValue);
                }
            }
        }

        protected override MesContableEditDto InternalValue { get; set; }

        #endregion Protected Properties

        #region Public Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            if(ParentValidation != null)
                InitializeValidation();
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (InternalValue != null)
            {
                InternalValue.OnChange -= InternalValue_OnChange;
            }
            base.Dispose(disposing);
        }

        protected override Task OnInternalValueChanged(MesContableEditDto value)
        {
            if (InternalValue != null && ParentValidation != null)
            {
                ParentValidation.Validate();
                StateHasChanged();
            }

            return ValueChanged.InvokeAsync(InternalValue);
        }

        protected override Task<ParseValue<MesContableEditDto>> ParseValueFromStringAsync(string value)
        {
            return Task.FromResult(new ParseValue<MesContableEditDto>(true, new MesContableEditDto()));
        }

        #endregion Protected Methods

        #region Private Methods

        private void InternalValue_OnChange()
        {
            if (exitEvent) return;

            exitEvent = true;
            if (InternalMes != InternalValue.Mes)
                InternalMes = InternalValue.Mes;

            if (InternalAño != InternalValue.Año.ToString())
                InternalAño = InternalValue.Año.ToString();
            exitEvent = false;
        }

        #endregion Private Methods
    }
}