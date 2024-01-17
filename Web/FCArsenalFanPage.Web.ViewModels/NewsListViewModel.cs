namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;

    public class NewsListViewModel
    {

        public NewsListViewModel()
        {
            this.News = new List<NewsInListViewModel>();
        }

        public IEnumerable<NewsInListViewModel> News { get; set; }

        public int PageNumber { get; set; }
    }
}
