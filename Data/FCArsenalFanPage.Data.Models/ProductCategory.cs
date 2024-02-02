namespace FCArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Common.Models;

    public class ProductCategory : BaseDeletableModel<int>
    {
        public ProductCategory()
        {
           this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
