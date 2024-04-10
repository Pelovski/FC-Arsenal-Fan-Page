namespace FCArsenalFanPage.Web.Controllers
{
	using System.Linq;
	using System.Security.Claims;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;

        public OrdersController(
            IProductService productService,
            IOrderService orderService)
        {
            this.productService = productService;
            this.orderService = orderService;
        }

        public IActionResult Cart()
        {
            var viewModel = this.orderService.GetAll().Where(x => x.Status == "Active");

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Order(ProductOrderInputViewModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var quantity = input.Quantity;
            var product = this.productService.GetById<CreateOrderInputModel>(input.Id);

            await this.orderService.CreateAsync(product, userId, quantity);

            return this.Redirect("/Shop/All");
        }
    }
}
