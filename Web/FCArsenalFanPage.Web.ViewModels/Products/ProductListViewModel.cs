namespace FCArsenalFanPage.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductListViewModel : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }

    }
}
