namespace FCArsenalFanPage.Data.Models
{
    using System;

    using FCArsenalFanPage.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public int NewsId { get; set; }

        public virtual News News { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
