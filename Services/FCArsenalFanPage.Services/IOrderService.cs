namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels.Orders;

    public interface IOrderService
    {
        Task CreateAsync(CreateOrderInputModel input, string userId, int quantity);
    }
}
