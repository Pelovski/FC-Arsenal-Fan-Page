
namespace FCArsenalFanPage.Services
{
    using System;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;
        private readonly IDeletableEntityRepository<Image> imageRepositoy;

        public NewsService(
            IDeletableEntityRepository<News> newsRepository,
            IDeletableEntityRepository<Image> imageRepositoy)
        {
            this.newsRepository = newsRepository;
            this.imageRepositoy = imageRepositoy;
        }

        public Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath)
        {

            //Content
            //CategoryId
            //Title


            return Task.CompletedTask;
        }
    }
}
