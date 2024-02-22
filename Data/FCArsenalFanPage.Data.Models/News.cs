namespace FCArsenalFanPage.Data.Models
{
    using FCArsenalFanPage.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
