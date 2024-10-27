namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using FCArsenalFanPage.Web.ViewModels.News;

    public interface INewsService
    {
        IEnumerable<NewsInListViewModel> GetAllWithDynamicPaging(int page, int firstPageItems, int otherPagesItems);

        IEnumerable<NewsInListViewModel> GetAllWithPaging(int page, int itemsPerPage = 6);

        Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath);

        Task UpdateAsync(int id, EditNewsInputViewModel input);

        IEnumerable<NewsInListViewModel> RecentPosts(int id);

        public IEnumerable<NewsDashboardViewModel> GetNewsForDashboard();

        IEnumerable<NewsInListViewModel> GetAll();

        Task DeleteAsync(int id);

        T GetById<T>(int id);

        int GetCount();
    }
}
