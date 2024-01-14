
namespace FCArsenalFanPage.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels;
    using Microsoft.AspNetCore.Http;

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

        public async Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath)
        {
            Directory.CreateDirectory($"{imagePath}/News/");

            var news = new News
            {
                Title = input.Title,
                Content = input.Content,
                CreatedByUserId = userId,
                CategoryId = input.CategoryId,
            };

            var image = input.Image;

            var imageExtension = Path.GetExtension(image.FileName).TrimStart('.');

            news.Image = new Image
            {
                News = news,
                Extension = imageExtension,
            };

            var imageId = news.Image.Id;

            var physicalPath = $"{imagePath}/News/{imageId}.{imageExtension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            await this.newsRepository.AddAsync(news);
            await this.newsRepository.SaveChangesAsync();
        }
    }
}
