namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System;

    using Ganss.Xss;

    public class NewsCommentViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(Content);

        public string UserUserName { get; set; }
    }
}
