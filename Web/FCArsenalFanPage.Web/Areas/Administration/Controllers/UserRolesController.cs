namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.Controllers;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class UserRolesController : BaseController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public UserRolesController(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
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
                .ToList();

            return this.View(viewModel);
        }


    }
}
