namespace FCArsenalFanPage.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int NewsId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
