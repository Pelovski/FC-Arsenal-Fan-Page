namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Products;

    public interface IProductService
    {
        Task CreateAsync(CreateProductInputModel input, string userId, string imagePath);

        IEnumerable<ProductInListViewModel> GetAll();

        T GetById<T>(string id);

        Task UpdateAsync(string id, EditProductInputViewModel input);
    }
}
