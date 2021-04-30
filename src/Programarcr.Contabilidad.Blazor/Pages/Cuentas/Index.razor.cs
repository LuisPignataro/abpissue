using Blazorise;
using Microsoft.AspNetCore.Components;
using Programarcr.Contabilidad.Configuracion;
using Programarcr.Contabilidad.Cuentas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components;

namespace Programarcr.Contabilidad.Blazor.Pages.Cuentas
{
    public partial class IndexModel : ContabilidadComponentBase
    {
        [Inject] 
        protected ICuentasAppService cuentasAppService { get; set; }
        public IList<CuentaDto> ExpandedNodes = new List<CuentaDto>();
        public IEnumerable<CuentaDto> Cuentas;
        public IEnumerable<CuentaDto> DataPanel = new List<CuentaDto>();
        public CuentaDto selectedNode;
        public ModalCreate ModalCreateComponent;
        

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            //await SetBreadcrumbItemsAsync();
            //await SetPermissionsAsync();

            Cuentas = await cuentasAppService.GetRootListAsync();
            
            foreach (var cuenta in Cuentas)
            {
                cuenta.Children = await cuentasAppService.GetChildListAsync(cuenta.Id);
            }
        }

        public void TreeNodeChanged(CuentaDto item)
        {
            Console.WriteLine($"TreeNodeChanged {item.Id}");
            GetChild(item);
            DataPanel = item.Children;
            
            selectedNode = item;
        }

        protected ICollection<CuentaDto> GetChildren(CuentaDto cuenta)
        {
            Console.WriteLine("GetChildren");
            GetChild(cuenta);

            Console.WriteLine($"GetChildren devuelve {cuenta.Children.Count} items");
            
            selectedNode = cuenta;

            return cuenta.Children;
        }

        private void GetChild(CuentaDto cuenta)
        {
            Console.WriteLine($"GetChild {cuenta.Id}");
            if (cuenta.HasChildren && cuenta.Children?.Count == 0)
            {
                Task.Run(async () =>
                {
                    Console.WriteLine("Iniciando tarea");
                    cuenta.Children = await cuentasAppService.GetChildListAsync(cuenta.Id);
                    Console.WriteLine($"Cargadas {cuenta.Children.Count}");
                    StateHasChanged();
                });
            }
        }
        
        protected async Task ShowCreateAccountComponent()
        {
            await ModalCreateComponent.ShowModal(selectedNode);
        }

        protected void NewAccuntIsCreated(CuentaDto paretAccount)
        {
            Console.WriteLine($"NUEVA CUENTA REVIBIDA POR INDEX");
            Console.WriteLine($"{paretAccount.Nombre}-{paretAccount.Id}");
            GetChild(paretAccount);
        }
        
    }

    
    
    public class CreateCuentaViewModel : IValidatableObject
    {
        public CreateCuentaViewModel()
        {
            Parent = new CuentaDto();
        }
        public CuentaDto Parent { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string MonedaId { get; set; }

        [Required]
        [EnumDataType(typeof(BalanceTypeEnum))]
        public BalanceTypeEnum TipoBalance { get; set; }

        [Required]
        [EnumDataType(typeof(CuentaTypeEnum))]
        public CuentaTypeEnum TipoCuenta { get; set; }

        [Required]
        public bool UtilizaCentroCostos { get; set; }

        [Required]
        public string Nombre { get; set; }

        
        
        // this validation not works
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Console.WriteLine("Validaciones");
            var cuentasAppService = validationContext.GetService(typeof(ICuentasAppService)) as ICuentasAppService;
            var cuentaFormatProvider = validationContext.GetService(typeof(ICuentaFormatProvider)) as ICuentaFormatProvider;
            
            Console.WriteLine("Numero de cuenta que se creara");
            var nuevaCuenta = $"{cuentaFormatProvider.GetPrefixAccountFromParent(Parent.Id)}{Numero}".PadRight(21, '0');
            Console.WriteLine(nuevaCuenta);
            if (cuentasAppService.AlreadyExistsAccount(nuevaCuenta))
            {
                yield return new ValidationResult("El numero de cuenta ya existe", new string[] { nameof(Numero) });
                
            }
            
            // if (cuentaFormatProvider.GetLevelOf(Numero) != (cuentaFormatProvider.GetLevelOf(Parent.Id) + 1))
            // {
            //     yield return new ValidationResult("El numero de cuenta no es valido", new string[] { nameof(Numero) });
            // }
        }
        
    }
}
