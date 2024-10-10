namespace FCArsenalFanPage.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly INewsService newsService;
        private readonly IOrderStatusService orderService;
        private readonly IProductService productService;
        private readonly IApplicationUserService userService;

        public DashboardService(
            INewsService newsService,
            IOrderStatusService orderService,
            IProductService productService,
            IApplicationUserService userService)
        {
            this.newsService = newsService;
            this.orderService = orderService;
            this.productService = productService;
            this.userService = userService;
        }

        public int NewsCount()
        {
            return this.newsService.GetCount();
        }

        public int OrdersCount()
        {
           return this.orderService.GetCount();
        }

        public int ProductsCount()
        {
           return this.productService.GetCount();
        }

        public int UsersCount()
        {
            return this.userService.GetCount();
        }
    }
}
