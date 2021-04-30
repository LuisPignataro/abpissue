using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;

namespace Programarcr.Contabilidad.IdentityServer
{
    public class IdentityRolesDataSeddContributor : IDataSeedContributor, ITransientDependency
    {
        public IdentityRolesDataSeddContributor(IdentityRoleManager roleManager, IGuidGenerator guidGenerator, IdentityUserManager userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            GuidGenerator = guidGenerator;
        }

        protected IdentityRoleManager RoleManager { get; }
        protected IdentityUserManager UserManager { get; }
        protected IGuidGenerator GuidGenerator { get;  }

        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedRoleAsync("Contador");
        }

        private async Task SeedRoleAsync(string name)
        {
            var role = await RoleManager.FindByNameAsync(name);

            if (role == null)
            {
                var r = await RoleManager.CreateAsync(new IdentityRole(GuidGenerator.Create(), name));

                if (r.Succeeded)
                {
                    var user = await UserManager.FindByNameAsync("admin");
                    if (user != null)
                    {
                        await UserManager.AddToRoleAsync(user, name);
                    }
                }

            }
        }
    }
}
