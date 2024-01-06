using FCArsenalFanPage.Data.Common.Repositories;
using FCArsenalFanPage.Data.Models;
using FCArsenalFanPage.Web.ViewModels;

namespace FCArsenalFanPage.Services
{
    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public CreateNewsInputModel Create()
        {
            throw new System.NotImplementedException();
        }
    }
}
