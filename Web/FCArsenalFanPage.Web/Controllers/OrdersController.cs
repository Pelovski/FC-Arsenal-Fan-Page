namespace FCArsenalFanPage.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class OrdersController : BaseController
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService userService;
        private readonly IAddressService addressService;
        private readonly IOrderStatusService orderStatusService;

        public OrdersController(
            IProductService productService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IApplicationUserService userService,
            IAddressService addressService,
            IOrderStatusService orderStatusService)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
            this.userService = userService;
            this.addressService = addressService;
            this.orderStatusService = orderStatusService;
        }

        [Authorize]
        public IActionResult Cart()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var viewModel = this.orderService
                .GetAll()
                .Where(x => x.UserId == userId && x.Status == "Cart");

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

            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            };

            this.Response.Cookies.Append("SuccessMessage", $"{product.Name} has been added successfully!", option);

            return this.RedirectToAction("All", "Shop");
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

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.orderService.GetOrderData(user);

            if (viewModel == null)
            {
                return this.Redirect("Cart");
            }

            if (!viewModel.Addresses.Any())
            {
                viewModel.ErrorMessage = "Please enter your address to proceed.";
                return this.View(viewModel);
            }

            this.HttpContext.Session.SetString("Orders", JsonConvert.SerializeObject(viewModel.Orders));

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddAddress(CheckoutViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var data = this.orderService.GetOrderData(user);

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // Set and chek if address alredy exist
            var newAddress = await this.userService.SetAddressToUserAsync(user, input.Street, input.Country, input.City, input.PostalCode);

            if (!newAddress)
            {
                input.ErrorMessage = "This address already exists. Please enter another one.";
                input.Addresses = data.Addresses;
                input.Orders = data.Orders;
                input.TotalPrice = data.TotalPrice;

                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Checkout));
        }

        [Authorize]
        public async Task<IActionResult> MyOrders()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.orderStatusService.GetAllOrderStatuses(user.Id);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus(OrderStatusViewModel input)
        {
            var areOrdersEmpty = !this.HttpContext.Session.TryGetValue("Orders", out byte[] ordersBytes);

            var user = await this.userManager.GetUserAsync(this.User);

            if (input.AddressId == null)
            {
                return this.RedirectToAction(nameof(this.Checkout));
            }

            if (!areOrdersEmpty)
            {
                var ordersJson = Encoding.UTF8.GetString(ordersBytes);
                var orders = JsonConvert.DeserializeObject<List<OrdersInListViewModel>>(ordersJson);

                input.Orders = orders;

                await this.orderStatusService.CreateAsync(input.AddressId, input.PaymentMethod, orders);

                await this.orderService.DeleteAllFromCartAsync(user.Id);
            }

            return this.RedirectToAction(nameof(this.MyOrders));
        }
    }
}
