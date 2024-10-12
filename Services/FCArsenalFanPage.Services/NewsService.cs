namespace FCArsenalFanPage.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.Encodings.Web;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using FCArsenalFanPage.Web.ViewModels.News;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<NewsInListViewModel> GetAllWithPaging(int page, int itemsPerPage = 6)
        {
            return this.GetAll()
                   .Skip((page - 1) * itemsPerPage)
                   .Take(itemsPerPage);
        }

        public IEnumerable<NewsInListViewModel> GetAllWithDynamicPaging(int page, int firstPageItems, int otherPagesItems)
        {
            int itemsPerPage = page == 1 ? firstPageItems : otherPagesItems;

            int skip = (page - 1) * otherPagesItems;
            if (page > 1)
            {
                skip -= otherPagesItems - firstPageItems;
            }

            return this.GetAll()
                       .Skip(skip)
                       .Take(itemsPerPage);
        }

        public IEnumerable<NewsInListViewModel> GetAll()
        {
            return this.newsRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Select(x => new NewsInListViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    UserName = x.User.UserName,
                    CategoryId = x.CategoryId,
                    CreatedOn = x.CreatedOn.ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture),
                    Content = x.Content,
                    ImageUrl = x.Image.RemoteImageUrl ?? "/Images/News/" + x.Image.Id + "." + x.Image.Extension,
                })
                .ToList();
        }

        public int GetCount()
        {
            return this.newsRepository.All().Count();
        }

        public T GetById<T>(int id)
        {
            var news = this.newsRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return news;
        }

        public async Task UpdateAsync(int id, EditNewsInputViewModel input)
        {
            var news = this.newsRepository.All().FirstOrDefault(x => x.Id == id);

            news.Title = input.Title;
            news.Content = input.Content;
            news.CategoryId = input.CategoryId;
            news.UserId = input.CreatedByUserId;

            await this.newsRepository.SaveChangesAsync();
        }

        public IEnumerable<NewsInListViewModel> RecentPosts(int id)
        {
            return this.GetAll()
                .Where(x => x.Id != id)
                .OrderByDescending(x => x.CreatedOn)
                .Take(4);
        }

        public IEnumerable<NewsViewModel> GetNewsForDashboard()
        {
            return this.newsRepository
                .AllAsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.User)
                .Select(x => new NewsViewModel
                {
                    NewsId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    UserName = x.User.UserName,
                    CategoryName = x.Category.Name,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn.Value.ToString("dd/MM/yyyy") : string.Empty,
                }).ToList();
        }
    }
}
