namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;

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

        public async Task CreateAsync(string addressId, string paymentMethod, IEnumerable<OrdersInListViewModel> orders)
        {
            var totalPrice = this.orderService.GetTotalPrice(orders);
            var userId = orders.First().UserId;
            var currentOrders = this.orderService.GetAllOrdersByUserId(userId);
            var orderNumber = this.orderService.GenerateOrderNumber();


            var orderStatus = new OrderStatus
            {
                AddressId = addressId,
                PaymentMethod = paymentMethod,
                UserId = userId,
                Orders = currentOrders,
                TotalPrice = totalPrice,
                OrderNumber = orderNumber,
            };

            await this.orderStatusRepository.AddAsync(orderStatus);
            await this.orderStatusRepository.SaveChangesAsync();

        }

        public IEnumerable<MyOrderViewModel> GetAllOrderStatuses(string userId)
        {
            // TODO: Get orders by order status id

            var oderStatuses = this.orderStatusRepository
                .All();

                

            return this.orderStatusRepository
                .AllAsNoTracking()
                .Select(x => new MyOrderViewModel
                {
                    OrderNumber = x.OrderNumber,
                    CreatedOn = x.CreatedOn.ToString(),
                    TotalPrice = x.TotalPrice,
                });
        }
    }
}
