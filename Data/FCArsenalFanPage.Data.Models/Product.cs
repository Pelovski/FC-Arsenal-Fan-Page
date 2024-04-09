namespace FCArsenalFanPage.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public string CartId { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }
    }
}
