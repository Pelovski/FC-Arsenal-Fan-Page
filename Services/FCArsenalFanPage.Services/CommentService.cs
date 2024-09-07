namespace FCArsenalFanPage.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task Create(int newsId, string userId, string content, int? parentId = null)
        {
            var comment = new Comment
            {
                Content = content,
                ParentId = parentId,
                NewsId = newsId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public bool IsInNewsId(int commentId, int newstId)
        {
            var commentNewsId = this.commentsRepository.All().Where(x => x.Id == commentId)
                .Select(x => x.NewsId).FirstOrDefault();

            return commentNewsId == newstId;
        }
    }
}
