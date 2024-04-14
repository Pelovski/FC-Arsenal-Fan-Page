namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
	using FCArsenalFanPage.Web.ViewModels.Orders;
	public interface IOrderService
    {
        Task CreateAsync(CreateOrderInputModel input, string userId, int quantity);

        IEnumerable<OrdersInListViewModel> GetAll();

        Task UpdateAsync(UpdateOrderInputModel input);
	}
}
