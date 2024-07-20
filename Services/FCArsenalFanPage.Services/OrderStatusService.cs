namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Microsoft.EntityFrameworkCore;

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
            var model = this.orderStatusRepository
                .All()
                .Include(x => x.Orders)
                .Where(x => x.UserId == userId)
                .Select(x => new MyOrderViewModel
                {
                    OrderNumber = x.OrderNumber,
                    CreatedOn = x.CreatedOn.ToString(),
                    TotalPrice = x.TotalPrice,
                    Orders = x.Orders
                            .Where(o => o.Status.Id == x.Id)
                            .Select(o => new OrdersInListViewModel
                            {
                                ProductName = o.Product.Name,
                                Price = o.Product.Price * o.Quantity,
                                Quantity = o.Quantity,
                                ImageUrl = o.Product.Image.RemoteImageUrl ?? "/Images/Products/" + o.Product.Image.Id + "." + o.Product.Image.Extension,
                            }).ToList(),

                }).ToList();

            return model;
        }
    }
}
