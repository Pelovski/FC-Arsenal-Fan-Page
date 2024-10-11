namespace FCArsenalFanPage.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FCArsenalFanPage.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Comments = new HashSet<Comment>();
        }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
