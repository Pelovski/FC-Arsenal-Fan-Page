namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
	using System.Threading.Tasks;
	using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.Controllers;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class UserRolesController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleService;

        public UserRolesController(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRoleService roleService)
        {
            this.userManager = userManager;
            this.roleService = roleService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var users = this.userManager.Users.ToList();

            // TODO: Service GetAllUsersWithRole
            var viewModel = users
                .SelectMany(user => this.userManager.GetRolesAsync(user)
                    .Result
                    .Select(role => new UserRolesViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = role,
                        CreatedOn = user.CreatedOn,
                        Email = user.Email,
                    }))
                .Where(x => x.Role != "Administrator")
                .ToList();

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
