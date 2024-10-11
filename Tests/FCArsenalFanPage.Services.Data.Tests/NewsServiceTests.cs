namespace FCArsenalFanPage.Services.Data.Tests
{
	using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class NewsServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<News>> mockNewsRepository;
        private readonly NewsService newsService;

        public NewsServiceTests()
        {
            this.mockNewsRepository = new Mock<IDeletableEntityRepository<News>>();
            this.newsService = new NewsService(this.mockNewsRepository.Object);
        }

        [Fact]
        public void GetAllShouldReturnCorrectNumberOfNewsItems()
        {
            var newsData = new List<News>
    {
        new News
        {
            Id = 1,
            Title = "News 1",
            Content = "Content 1",
            User = new ApplicationUser { UserName = "User1" },
            Category = new Category { Id = 1, Name = "Category1" },
            Image = new Image { Id = "image1", Extension = "jpg" },
        },
        new News
        {
            Id = 2,
            Title = "News 2",
            Content = "Content 2",
            User = new ApplicationUser { UserName = "User2" },
            Category = new Category { Id = 2, Name = "Category2" },
            Image = new Image { Id = "image2", Extension = "jpg" },
        },
        new News
        {
            Id = 3,
            Title = "News 3",
            Content = "Content 3",
            User = new ApplicationUser { UserName = "User3" },
            Category = new Category { Id = 3, Name = "Category3" },
            Image = new Image { Id = "image3", Extension = "jpg" },
        },
    }.AsQueryable();

            this.mockNewsRepository.Setup(x => x.AllAsNoTracking()).Returns(newsData);

            var result = this.newsService.GetAll();

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task CreateAsyncShouldAddNewsToRepository()
        {
            var mockFile = new Mock<IFormFile>();
            var content = "Fake file content";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockFile.Setup(f => f.OpenReadStream()).Returns(ms);
            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(ms.Length);
            mockFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                    .Returns((Stream target, CancellationToken token) =>
                    {
                        ms.CopyTo(target);
                        return Task.CompletedTask;
                    });

            var createNewsModel = new CreateNewsInputModel
            {
                Title = "New News",
                Content = "New Content",
                CategoryId = 1,
                Image = mockFile.Object,  // Set the mock file here
            };

            this.mockNewsRepository.Setup(x => x.AddAsync(It.IsAny<News>())).Returns(Task.CompletedTask);

            await this.newsService.CreateAsync(createNewsModel, "test-user", "/images");

            this.mockNewsRepository.Verify(x => x.AddAsync(It.IsAny<News>()), Times.Once);
            this.mockNewsRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateNewsInRepository()
        {
            var news = new News { Id = 1, Title = "Old Title", Content = "Old Content", CategoryId = 1 };
            var updatedNews = new EditNewsInputViewModel { Title = "New Title", Content = "New Content", CategoryId = 2, CreatedByUserId = "user1" };

            this.mockNewsRepository.Setup(x => x.All()).Returns(new List<News> { news }.AsQueryable());

            await this.newsService.UpdateAsync(1, updatedNews);

            Assert.Equal("New Title", news.Title);
            Assert.Equal("New Content", news.Content);
            Assert.Equal(2, news.CategoryId);
            this.mockNewsRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void GetAllShouldReturnLimitedSentences()
        {
            var mockRepo = new Mock<IDeletableEntityRepository<News>>();
            var sampleNews = new List<News>
        {
            new News
            {
                Id = 1,
                Title = "Sample News 1",
                Content = "This is the first sentence. This is the second sentence. This is the third sentence. This is the fourth sentence.",
                User = new ApplicationUser { UserName = "User1" },
                CategoryId = 1,
                CreatedOn = System.DateTime.Now,
                Image = new Image { Id = "1", Extension = "jpg" },
            },
        }.AsQueryable();

            mockRepo.Setup(r => r.AllAsNoTracking()).Returns(sampleNews);

            var service = new NewsService(mockRepo.Object);

            var result = service.GetAll().FirstOrDefault();

            Assert.NotNull(result);
            Assert.Equal("This is the first sentence. This is the second sentence. This is the third sentence.", result.Details);
        }

        [Fact]
        public void GetCountShouldReturnCorrectCount()
        {
            var newsList = new List<News>
        {
            new News { Id = 1, Title = "News 1" },
            new News { Id = 2, Title = "News 2" },
        }.AsQueryable();

            this.mockNewsRepository.Setup(r => r.All()).Returns(newsList);

            var count = this.newsService.GetCount();

            Assert.Equal(2, count);
        }
    }
}
