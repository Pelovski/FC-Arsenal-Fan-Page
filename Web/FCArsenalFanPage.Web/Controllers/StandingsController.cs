namespace FCArsenalFanPage.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class StandingsController : BaseController
    {
        public IActionResult Leaderboard()
        {
            return this.View();
        }
    }
}
