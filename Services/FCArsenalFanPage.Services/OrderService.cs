namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;

    public class OrderService : IOrderService
    {
        private readonly IDeletableEntityRepository<Order> orderRepository;
        private readonly IAdressService adressService;

        public OrderService(
            IDeletableEntityRepository<Order> orderRepository,
            IAdressService adressService)
        {
            this.orderRepository = orderRepository;
            this.adressService = adressService;
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

        public async Task DeleteAsync(string id)
        {
            var order = this.orderRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id);

            if (order != null)
            {
                this.orderRepository.Delete(order);
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

        public IEnumerable<OrdersInListViewModel> GetAllByUserId(string userId)
        {
            return this.GetAll()
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public int GetCountByUserId(string userId)
        {
            return this.orderRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == userId)
                .Select(x => x.Quantity)
                .Sum();
        }

        public Order GetCurrentOrder(string userId, string productId)
        {
            return this.orderRepository
                .All()
                .Where(x => x.UserId == userId && x.Id == productId)
                .FirstOrDefault();
        }

        public double GetTotalPrice(IEnumerable<OrdersInListViewModel> orders)
        {
            var deliveryPrice = 25;

            return orders
                .Select(x => x.TotalOrderPrice)
                .Sum() + deliveryPrice;
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

        public CheckoutViewModel GetOrderData(ApplicationUser user)
        {

            var orders = this.GetAllByUserId(user.Id);
            var adresses = this.adressService.GetAdressesByUser(user);


            if (!orders.Any())
            {
                return null;
            }


            var totalPrice = this.GetTotalPrice(orders);

            var result = new CheckoutViewModel()
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Orders = orders,
                Adresses = adresses,
                TotalPrice = totalPrice,
            };

            return result;
        }
    }
}
