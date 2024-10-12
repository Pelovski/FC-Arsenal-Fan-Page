namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;

    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ApplicationUsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUserRepository;

        public ApplicationUsersController(
            UserManager<ApplicationUser> userManager,
            IRoleService roleService,
            IApplicationUserService applicationUserService,
            IDeletableEntityRepository<ApplicationUser> applicationUserRepository)
        {
            this.userManager = userManager;
            this.roleService = roleService;
            this.applicationUserService = applicationUserService;
            this.applicationUserRepository = applicationUserRepository;
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

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || this.applicationUserRepository.All() == null)
            {
                return this.NotFound();
            }

            var viewModel = this.applicationUserService
                .GetAllUsersWithRole()
                .Where(x => x.UserId == id)
                .FirstOrDefault();


            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }
    }
}
