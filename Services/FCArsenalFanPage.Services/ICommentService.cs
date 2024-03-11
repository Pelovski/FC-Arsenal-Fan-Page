namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task Create(int newsId, string userId, string content, int? parentId = null);
    }
}
