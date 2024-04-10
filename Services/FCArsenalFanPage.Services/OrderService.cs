namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public OrderService(
            IDeletableEntityRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task CreateAsync(CreateOrderInputModel input, string userId, int quantity)
        {

            var orderStatus = new OrderStatus
            {
                Name = "Active",
            };

            var order = new Order
            {
                ProductId = input.Id,
                Quantity = quantity,
                UserId = userId,
                Status = orderStatus,
            };

            //TODO: OrderStatus and SeedRoles

            await this.orderRepository.AddAsync(order);
            await this.orderRepository.SaveChangesAsync();
        }
    }
}
