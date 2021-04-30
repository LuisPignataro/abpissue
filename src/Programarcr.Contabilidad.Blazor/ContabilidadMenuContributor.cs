using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Programarcr.Contabilidad.Localization;
using Programarcr.Contabilidad.Permissions;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Programarcr.Contabilidad.Blazor
{
    public class ContabilidadMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public ContabilidadMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<ContabilidadResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
            var AuthorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    "Contabilidad.Home",
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            if (currentUser.IsAuthenticated)
            {
                var configMenu = new ApplicationMenuItem(
                        "Config",
                        l["Menu:Config"],
                        "/",
                        icon: "fas fa-cog"
                    ).AddItem(new ApplicationMenuItem("Config.Monedas", l["Menu:Config.Monedas"], url: "/configuracion/monedas"));

                if (await AuthorizationService.IsGrantedAsync(ContaPermissions.Cotizacion.Default))
                    configMenu.AddItem(new ApplicationMenuItem("Config.Cotizacion",l["Menu:Config.Cotizacion"],"/configuracion/cotizacion"));
                
                if(await AuthorizationService.IsGrantedAsync(ContaPermissions.Account.Plan))
                    configMenu.AddItem(new ApplicationMenuItem("Cuentas", l["Menu:Account"], url: "/cuentas"));

                if (await AuthorizationService.IsGrantedAsync(ContaPermissions.CentroDeCostos.Default))
                    configMenu.AddItem(new ApplicationMenuItem("CentroCosto", l["Menu:CentroCosto"], url: "/configuracion/Centrodecostos"));

                if (await AuthorizationService.IsGrantedAsync(ContaPermissions.PeriodoContable.Default))
                    configMenu.AddItem(new ApplicationMenuItem("PeriodosContables", l["Menu:PeriodosContables"], url: "/configuracion/periodos"));

                context.Menu.Items.Insert(1,configMenu);
            }
           
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            if (currentUser.IsAuthenticated)
            {
                context.Menu.AddItem(new ApplicationMenuItem(
                    "Account.Manage",
                    accountStringLocalizer["ManageYourProfile"],
                    $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage",
                    icon: "fa fa-cog",
                    order: 1000,
                    null,
                    "_blank"));
            }

            return Task.CompletedTask;
        }
    }
}
