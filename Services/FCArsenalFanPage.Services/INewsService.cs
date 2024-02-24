namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.News;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath);

        IEnumerable<NewsInListViewModel> GetAll();

        IEnumerable<NewsInListViewModel> GetAllWithPaging(int page, int itemsPerPage = 6);

        int GetCount();

        T GetById<T>(int id);
    }
}
