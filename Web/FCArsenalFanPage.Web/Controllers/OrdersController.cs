namespace FCArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class OrdersController : BaseController
    {
        [Authorize]
        public IActionResult Cart()
        {
            return this.View();
        }
    }
}
