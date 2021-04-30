using Programarcr.Contabilidad.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Programarcr.Contabilidad.Permissions
{
    public class ContabilidadPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var Config = context.AddGroup(ContaPermissions.ConfigPermissionGroup, L("Permission:Config"));

            var configPermission = Config.AddPermission(ContaPermissions.Monedas.Default, L("Permission:Monedas"));
            configPermission.AddChild(ContaPermissions.Monedas.CreateEdit, L("Permission:MonedasCreateEdit"));

            var centroCostosPermission = Config.AddPermission(ContaPermissions.CentroDeCostos.Default, L("Permission:CentroDeCostos"));
            centroCostosPermission.AddChild(ContaPermissions.CentroDeCostos.CreateEdit, L("Permission:CentroDeCostosCreateEdit"));

            
            var cotizacionesPermisions = Config.AddPermission(ContaPermissions.Cotizacion.Default, L("Permission:Cotizaciones.Permision"));
            cotizacionesPermisions.AddChild(ContaPermissions.Cotizacion.CreateEdit, L("Permission:Cotizaciones:CreateEdit"));
            cotizacionesPermisions.AddChild(ContaPermissions.Cotizacion.Delete, L("Permission:Cotizaciones:Delete"));

            Config.AddPermission(ContaPermissions.PeriodoContable.Default, L("Permission:Config.PeriodoContable"));

            var Account = context.AddGroup(ContaPermissions.AccountPermissionGroup, L("Permission:Account"));

            //Define your own permissions here. Example:
            var accountPermission = Account.AddPermission(ContaPermissions.Account.Default, L("Permission:AccountPlan"));
            accountPermission.AddChild(ContaPermissions.Account.CreateEdit, L("Permission:AccountCreateEdit"));
            accountPermission.AddChild(ContaPermissions.Account.Plan, L("Permission:AccountPlan"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<ContabilidadResource>(name);
        }
    }
}
