namespace FCArsenalFanPage.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
	using FCArsenalFanPage.Data.Models;
	using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
		private readonly UserManager<ApplicationUser> userManager;

		public OrdersController(
            IProductService productService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.orderService = orderService;
			this.userManager = userManager;
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

        public async Task<IActionResult> Delete(string id)
        {
           await this.orderService.DeleteAsync(id);

           return this.RedirectToAction(nameof(this.Cart));
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var orders = this.orderService.GetAllByUserId(user.Id);
            var totalPrice = this.orderService.GetTotalPrice(orders);

            var viewModel = new CheckoutViewModel
            {
                User = user,
                Orders = orders,
                TotalPrice = totalPrice,
            };

            return this.View(viewModel);
        }
    }
}
