namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.Controllers;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class UserRolesController : BaseController
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserRolesController(
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new UserRolesViewModel
            {
                Users = this.userRepository.All(),
            };

            return this.View(viewModel);
        }


    }
}
