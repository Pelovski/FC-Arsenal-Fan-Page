namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;

    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class UserRolesController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleService;
        private readonly IApplicationUserService applicationUserService;

        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            IRoleService roleService,
            IApplicationUserService applicationUserService)
        {
            this.userManager = userManager;
            this.roleService = roleService;
            this.applicationUserService = applicationUserService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = this.applicationUserService.GetAllUsersWithRole()
                .Where(x => x.Role != "Administrator");

            return this.View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var roles = this.roleService.GetAll();

            var viewModel = new EditUserRolesViewModel
            {
                UserId = id,
                Roles = roles,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserRolesViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Roles = this.roleService.GetAll();
                return this.View(input);
            }

            await this.roleService.UpdateAsync(input.UserId, input.RoleId);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
