namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly IDashboardService dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexAdminDashboardViewModel
            {
                NewsCount = this.dashboardService.NewsCount(),
                OrdersCount = this.dashboardService.OrdersCount(),
                ProductsCount = this.dashboardService.ProductsCount(),
                UsersCount = this.dashboardService.UsersCount(),
            };

            return this.View(viewModel);
        }
    }
}
