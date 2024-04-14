namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AngleSharp.Browser.Dom;
    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;
	using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            var currentOrder = this.orderRepository
                .All()
                .Where(x => x.UserId == userId && x.ProductId == input.Id)
                .FirstOrDefault();

            if (currentOrder == null)
            {
                var order = new Order
                {
                    ProductId = input.Id,
                    Quantity = quantity,
                    UserId = userId,
                    Status = orderStatus,
                };

                await this.orderRepository.AddAsync(order);
            }
            else
            {
                currentOrder.Quantity += quantity;
            }

            await this.orderRepository.SaveChangesAsync();
        }

        public IEnumerable<OrdersInListViewModel> GetAll()
        {
            return this.orderRepository.AllAsNoTracking()
                .Select(x => new OrdersInListViewModel
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    Price = x.Product.Price,
                    UserId = x.UserId,
                    Status = x.Status.Name,
                    ImageUrl = x.Product.Image.RemoteImageUrl ?? "/Images/Products/"
                    + x.Product.Image.Id + "." + x.Product.Image.Extension,
                });
        }

        public Order GetCurrentOrder(string userId, string productId)
        {
            return this.orderRepository
                .All()
                .Where(x => x.UserId == userId && x.Id == productId)
                .FirstOrDefault();
        }

        public async Task UpdateAsync(UpdateOrderInputModel input)
        {
            for (int i = 0; i < input.Id.Count(); i++)
            {
                var currentOrder = this.GetCurrentOrder(input.UserId, input.Id[i]);

                var orderQuantity = input.Quantity[i];

                if (currentOrder.Quantity != orderQuantity)
                {
                    currentOrder.Quantity = orderQuantity;
                }
            }

            await this.orderRepository.SaveChangesAsync();
        }
    }
}
