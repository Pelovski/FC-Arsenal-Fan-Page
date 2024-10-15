namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using FCArsenalFanPage.Web.ViewModels.Products;

    public interface IProductService
    {
        Task CreateAsync(CreateProductInputModel input, string userId, string imagePath);

        IEnumerable<ProductDashboardViewModel> GetProductsForDashboard();

        Task UpdateProductQuantityAsync(string productId, int quantity, bool isAdding);

        Task UpdateAsync(string id, EditProductInputViewModel input);

        IEnumerable<ProductInListViewModel> GetAll();

        T GetById<T>(string id);

        int GetCount();
    }
}
