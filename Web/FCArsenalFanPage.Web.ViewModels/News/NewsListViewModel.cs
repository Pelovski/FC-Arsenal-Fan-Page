namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;

    public class NewsListViewModel
    {
        public IEnumerable<NewsInListViewModel> News { get; set; }

        public bool HasPreviousPage => PageNumber > 1;

        public int PreviousPageNumber => PageNumber - 1;

        public bool HasNextPage => PageNumber < PagesCount;

        public int NextPageNumber => PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)NewsCount / ItemsPerPage);

        public int PageNumber { get; set; }

        public int NewsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
