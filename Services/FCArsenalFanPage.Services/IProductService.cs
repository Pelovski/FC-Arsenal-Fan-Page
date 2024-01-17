namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels;

    public interface IProductService
    {
        Task CreateAsync(CreateProductInputModel input, string userId, string imagePath);
    }
}
