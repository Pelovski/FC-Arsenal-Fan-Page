namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.IO;
	using System.Linq;
	using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsService(
            IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public async Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath)
        {
            Directory.CreateDirectory($"{imagePath}/News/");

            var news = new News
            {
                Title = input.Title,
                Content = input.Content,
                UserId = userId,
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

        public IEnumerable<NewsInListViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            var news = this.newsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new NewsInListViewModel
                {
                    Title = x.Title,
                    UserName = x.User.UserName,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn.ToString(),
                    Content = x.Content,
                    ImageUrl = x.Image.RemoteImageUrl != null ?
                               x.Image.RemoteImageUrl :
                              "/Images/News/" + x.Image.Id + "." + x.Image.Extension,
                })
                .ToList();

            return news;
        }
    }
}
