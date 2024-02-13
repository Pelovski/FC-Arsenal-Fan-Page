namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;

    public class IndexListViewModel
    {
        public IEnumerable<NewsInListViewModel> News { get; set; }

        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
