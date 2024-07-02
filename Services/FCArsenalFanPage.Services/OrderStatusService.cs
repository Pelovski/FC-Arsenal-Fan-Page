namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;

    public class OrderStatusService : IOrderStatusService
    {
        private readonly IDeletableEntityRepository<OrderStatus> orderStatusRepository;
        private readonly IOrderService orderService;

        public OrderStatusService(
            IDeletableEntityRepository<OrderStatus> orderStatusRepository,
            IOrderService orderService)
        {
            this.orderStatusRepository = orderStatusRepository;
            this.orderService = orderService;
        }

        public async Task CreateAsync(string addressId, string paymentMethod, string userId)
        {
            var orders = this.orderService.GetAllOrdersByUserId(userId);

            var orderStatus = new OrderStatus
            {
                AddressId = addressId,
                PaymentMethod = paymentMethod,
                UserId = userId,
                Orders = orders,
            };
        }
    }
}
