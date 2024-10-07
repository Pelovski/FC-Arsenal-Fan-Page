namespace FCArsenalFanPage.Web.Controllers
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentService commentService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel input)
        {

            var userId = this.userManager.GetUserId(this.User);
            var parentId = input.ParentId == 0 ?
                (int?)null :
                input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentService.IsInNewsId(parentId.Value, input.NewsId))
                {
                    return this.BadRequest();
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("SingleNews", "News", new { id = input.NewsId });
            }

            await this.commentService.Create(input.NewsId, userId, input.Content, parentId);

            return this.RedirectToAction("SingleNews", "News", new { id = input.NewsId });
        }
    }
}
