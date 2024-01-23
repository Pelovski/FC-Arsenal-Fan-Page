namespace FCArsenalFanPage.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class NewsListViewModel
    {

        public NewsListViewModel()
        {
            this.News = new List<NewsInListViewModel>();
        }

        public IEnumerable<NewsInListViewModel> News { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.NewsCount / this.ItemsPerPage);

        public int PageNumber { get; set; }

        public int NewsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
