namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Moq;
    using Xunit;

    public class CommentServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Comment>> mockCommentsRepository;
        private readonly CommentService commentService;

        public CommentServiceTests()
        {
            this.mockCommentsRepository = new Mock<IDeletableEntityRepository<Comment>>();
            this.commentService = new CommentService(this.mockCommentsRepository.Object);
        }

        [Fact]
        public async Task CreateShouldAddAndSaveComment()
        {
            // Arrange
            var newsId = 1;
            var userId = "user1";
            var content = "This is a comment";
            var parentId = (int?)null; // No parent comment

            // Act
            await this.commentService.Create(newsId, userId, content, parentId);

            // Assert
            this.mockCommentsRepository.Verify(
                repo => repo.AddAsync(It.Is<Comment>(c =>
                c.NewsId == newsId &&
                c.UserId == userId &&
                c.Content == content &&
                c.ParentId == parentId)), Times.Once);

            this.mockCommentsRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void IsInNewsIdShouldReturnTrueWhenCommentBelongsToNews()
        {
            // Arrange
            var commentId = 1;
            var newsId = 1;
            var comment = new Comment { Id = commentId, NewsId = newsId };

            this.mockCommentsRepository.Setup(repo => repo.All())
                .Returns(new List<Comment> { comment }.AsQueryable());

            // Act
            var result = this.commentService.IsInNewsId(commentId, newsId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsInNewsIdShouldReturnFalseWhenCommentDoesNotBelongToNews()
        {
            // Arrange
            var commentId = 1;
            var newsId = 2;
            var comment = new Comment { Id = commentId, NewsId = 1 };

            this.mockCommentsRepository.Setup(repo => repo.All())
                .Returns(new List<Comment> { comment }.AsQueryable());

            // Act
            var result = this.commentService.IsInNewsId(commentId, newsId);

            // Assert
            Assert.False(result);
        }
    }
}
