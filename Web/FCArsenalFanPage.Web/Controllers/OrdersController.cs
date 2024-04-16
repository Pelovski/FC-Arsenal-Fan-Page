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

        [Authorize]
        public IActionResult Cart()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = this.orderService
                .GetAll()
                .Where(x => x.Status == "Active" && x.UserId == userId);

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateOrderInputModel input)
        {

            await this.orderService.UpdateAsync(input);
            return this.RedirectToAction(nameof(this.Cart));
        }

        public IActionResult Checkout()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return this.View();
        }
    }
}
