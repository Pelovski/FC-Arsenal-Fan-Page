namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;

    public class NewsListViewModel
    {
        public IEnumerable<NewsInListViewModel> News { get; set; }

        public int PageNumber { get; set; }
    }
}
