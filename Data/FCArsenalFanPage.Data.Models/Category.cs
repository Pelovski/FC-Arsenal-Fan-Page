using System.ComponentModel.DataAnnotations;

namespace FCArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.News = new HashSet<News>();
        }

        [MaxLength(30)]
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
