namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;

    public interface IOrderService
    {
        Task CreateAsync(CreateOrderInputModel input, string userId, int quantity);

        IEnumerable<OrdersInListViewModel> GetAll();

        CheckoutViewModel GetOrderData(ApplicationUser user);

        Task UpdateAsync(UpdateOrderInputModel input);

        Task DeleteAsync(string id);

        int GetCountByUserId(string userId);

        double GetTotalPrice(IEnumerable<OrdersInListViewModel> orders);

        IEnumerable<OrdersInListViewModel> GetAllByUserId(string userId);

        ICollection<Order> GetAllOrdersByUserId(string userId);

        public string GenerateOrderNumber();

        Task RemoveAsync(string orderId);
    }
}
