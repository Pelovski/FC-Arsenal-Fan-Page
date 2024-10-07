namespace FCArsenalFanPage.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        public int NewsId { get; set; }

        public int ParentId { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }
    }
}
