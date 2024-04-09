namespace FCArsenalFanPage.Web.Controllers
{
	using System.Collections.Generic;
	using FCArsenalFanPage.Services;
	using FCArsenalFanPage.Web.ViewModels.Orders;
	using FCArsenalFanPage.Web.ViewModels.Products;
	using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        private readonly IProductService productService;

        public OrdersController(
            IProductService productService)
        {
            this.productService = productService;
        }
        public IActionResult Order(ProductOrderInputViewModel input)
        {
            var product = this.productService.GetById<CartItemViewModel>(input.Id);

            product.Quantity = input.Quantity;

            return this.View();
        }
    }
}
