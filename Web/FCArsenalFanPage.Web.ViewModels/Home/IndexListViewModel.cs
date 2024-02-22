namespace FCArsenalFanPage.Web.ViewModels.Home
{
    using System.Collections.Generic;
    using FCArsenalFanPage.Web.ViewModels.News;
    using FCArsenalFanPage.Web.ViewModels.Products;

    public class IndexListViewModel
    {
        public IEnumerable<NewsInListViewModel> News { get; set; }

        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
