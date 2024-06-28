﻿namespace FCArsenalFanPage.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Components.Forms;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IProductService productService;
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IApplicationUserService userService;
        private readonly IAdressService adressService;

        public OrdersController(
            IProductService productService,
            IOrderService orderService,
            UserManager<ApplicationUser> userManager,
            IApplicationUserService userService,
            IAdressService adressService)
        {
            this.productService = productService;
            this.orderService = orderService;
            this.userManager = userManager;
            this.userService = userService;
            this.adressService = adressService;
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

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.orderService.GetOrderData(user);

            if (viewModel == null)
            {
                return this.Redirect("Cart");
            }

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var data = this.orderService.GetOrderData(user);

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var newAdress = await this.userService.SetAdressToUserAsync(user, input.Street, input.Country, input.City, input.PostalCode);

            if (!newAdress)
            {
                input.ErrorMessage = "This address already exists. Please enter another one.";
                input.Adresses = data.Adresses;
                input.Orders = data.Orders;
                input.TotalPrice = data.TotalPrice;

                return this.View(input);
            }

            return this.RedirectToAction("Checkout");
        }
    }
}
