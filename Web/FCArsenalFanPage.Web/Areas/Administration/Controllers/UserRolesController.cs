namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.Controllers;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Rendering;
	using Microsoft.Extensions.DependencyInjection;

    [Area("Administration")]
    public class UserRolesController : BaseController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IServiceProvider serviceProvider;

        public UserRolesController(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            IServiceProvider serviceProvider)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var users = this.userManager.Users.ToList();

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
            //var user = await this.userManager.FindByIdAsync(id);

            var roles = this.serviceProvider.
                GetRequiredService<RoleManager<ApplicationRole>>()
                .Roles
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name,
                }).ToList();

            var viewModel = new EditUserRolesViewModel
            {
                Id = id,
                Roles = roles,
            };

            return this.View(viewModel);
        }
    }
}
