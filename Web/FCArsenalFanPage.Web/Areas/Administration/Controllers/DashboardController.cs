namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using FCArsenalFanPage.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
