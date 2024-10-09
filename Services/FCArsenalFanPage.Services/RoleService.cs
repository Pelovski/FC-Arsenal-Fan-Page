namespace FCArsenalFanPage.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.DependencyInjection;

    public class RoleService : IRoleService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleService(
            IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager)
        {
            this.serviceProvider = serviceProvider;
            this.userManager = userManager;
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            return this.serviceProvider.
                GetRequiredService<RoleManager<ApplicationRole>>()
                .Roles
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name,
                }).ToList();
        }

        public async Task UpdateAsync(string userId, string roleId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var currentRole = this.userManager.GetRolesAsync(user).Result.FirstOrDefault();
            var roleName = this.GetAll().FirstOrDefault(x => x.Value == roleId).Text;

            await this.userManager.RemoveFromRoleAsync(user, currentRole);

            await this.userManager.AddToRoleAsync(user, roleName);
        }
    }
}
