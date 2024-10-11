using System.ComponentModel.DataAnnotations;

namespace FCArsenalFanPage.Data.Models
{
    using FCArsenalFanPage.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int NewsId { get; set; }

        public virtual News News { get; set; }

        public int? ParentId { get; set; }

        public virtual Comment Parent { get; set; }

        [MaxLength(1000)]
		public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
