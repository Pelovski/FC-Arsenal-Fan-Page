namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath);

    }
}
