namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public void GetAllShouldTakeAllCategories()
        {
            var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category1" },
            new Category { Id = 2, Name = "Category2" },
        };

            var mockRepo = new Mock<IDeletableEntityRepository<Category>>();
            var service = new CategoriesService(mockRepo.Object);

            mockRepo.Setup(x => x.All())
                .Returns(categories.AsQueryable());

            var result = service.GetAll();

            Assert.NotNull(result);

            var selectListItems = result.ToList();

            Assert.Equal(2, selectListItems.Count);
            Assert.Contains(selectListItems, item => item.Value == "1" && item.Text == "Category1");
            Assert.Contains(selectListItems, item => item.Value == "2" && item.Text == "Category2");
        }
    }
}
