namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;

    public class ProductListViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }

        public int PageNumber { get; set; }
    }
}
