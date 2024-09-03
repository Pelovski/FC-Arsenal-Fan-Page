namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class OrderServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<Order>> orderRepositoryMock;
        private readonly Mock<IAddressService> addressServiceMock;
        private readonly OrderService orderService;

        public OrderServiceTests()
        {
            this.orderRepositoryMock = new Mock<IDeletableEntityRepository<Order>>();
            this.addressServiceMock = new Mock<IAddressService>();
            this.orderService = new OrderService(this.orderRepositoryMock.Object, this.addressServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsyncShouldAddOrderWhenOrderDoesNotExist()
        {
            // Arrange
            var input = new CreateOrderInputModel { Id = "Product1" };
            var userId = "User1";
            var quantity = 2;

            this.orderRepositoryMock.Setup(x => x.All())
                .Returns(new List<Order>().AsQueryable());

            // Act
            await this.orderService.CreateAsync(input, userId, quantity);

            // Assert
            this.orderRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
            this.orderRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = "Order1";
            var order = new Order { Id = orderId };

            this.orderRepositoryMock.Setup(x => x.All())
                .Returns(new List<Order> { order }.AsQueryable());

            // Act
            await this.orderService.DeleteAsync(orderId);

            // Assert
            this.orderRepositoryMock.Verify(x => x.Delete(order), Times.Once);
            this.orderRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void GetAllByUserIdShouldReturnCorrectOrdersWhenOrdersExistForUser()
        {
            // Arrange
            var userId = "User1";
            var orders = new List<Order>
    {
        new Order
        {
            Id = "Order1",
            UserId = userId,
            Product = new Product
            {
                Name = "Product1",
                Price = 100,
                Image = new Image
                {
                    RemoteImageUrl = null,
                    Id = "Image1",
                    Extension = "jpg",
                },
            },
        },
        new Order
        {
            Id = "Order2",
            UserId = "User2",
            Product = new Product
            {
                Name = "Product2",
                Price = 200,
                Image = new Image
                {
                    RemoteImageUrl = "http://example.com/image.jpg",
                    Id = "Image2",
                    Extension = "jpg",
                },
            },
        },
    };

            this.orderRepositoryMock.Setup(x => x.AllAsNoTracking())
                .Returns(orders.AsQueryable());

            // Act
            var result = this.orderService.GetAllByUserId(userId);

            // Assert
            Assert.Single(result);
            Assert.Equal("Order1", result.First().Id);
        }

        [Fact]
        public async Task DeleteAllFromCartAsyncShouldUpdateAllOrdersStatusToDoneWhenOrdersExist()
        {
            // Arrange
            var userId = "User1";
            var orders = new List<Order>
             {
                 new Order { Id = "Order1", UserId = userId, Status = "Cart" },
                 new Order { Id = "Order2", UserId = userId, Status = "Cart" },
             };

            this.orderRepositoryMock.Setup(x => x.All())
                .Returns(orders.AsQueryable());

            // Act
            await this.orderService.DeleteAllFromCartAsync(userId);

            // Assert
            Assert.All(orders, order => Assert.Equal("Done", order.Status));
            this.orderRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void GetAllShouldReturnCorrectOrdersInListViewModelWhenOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
    {
        new Order
        {
            Id = "Order1",
            Status = "Cart",
            Quantity = 2,
            UserId = "User1",
            Product = new Product
            {
                Name = "Product1",
                Price = 100,
                Image = new Image
                {
                    RemoteImageUrl = null,
                    Id = "Image1",
                    Extension = "jpg",
                },
            },
        },
        new Order
        {
            Id = "Order2",
            Status = "Done",
            Quantity = 1,
            UserId = "User2",
            Product = new Product
            {
                Name = "Product2",
                Price = 200,
                Image = new Image
                {
                    RemoteImageUrl = "http://example.com/image.jpg",
                    Id = "Image2",
                    Extension = "png",
                },
            },
        },
    };

            this.orderRepositoryMock.Setup(x => x.AllAsNoTracking())
                .Returns(orders.AsQueryable());

            // Act
            var result = this.orderService.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Equal("Order1", result[0].Id);
            Assert.Equal("Cart", result[0].Status);
            Assert.Equal("Product1", result[0].ProductName);
            Assert.Equal(100, result[0].Price);
            Assert.Equal(2, result[0].Quantity);
            Assert.Equal("User1", result[0].UserId);
            Assert.Equal("/Images/Products/Image1.jpg", result[0].ImageUrl); // Fallback URL

            Assert.Equal("Order2", result[1].Id);
            Assert.Equal("Done", result[1].Status);
            Assert.Equal("Product2", result[1].ProductName);
            Assert.Equal(200, result[1].Price);
            Assert.Equal(1, result[1].Quantity);
            Assert.Equal("User2", result[1].UserId);
            Assert.Equal("http://example.com/image.jpg", result[1].ImageUrl); // Remote URL
        }

        [Fact]
        public void GetAllOrdersByUserIdShouldReturnCorrectOrdersWhenOrdersExistForUser()
        {
            var userId = "User1";
            var orders = new List<Order>
    {
        new Order { Id = "Order1", UserId = userId },
        new Order { Id = "Order2", UserId = userId },
        new Order { Id = "Order3", UserId = "User2" },
    };

            this.orderRepositoryMock.Setup(x => x.All())
                .Returns(orders.AsQueryable());

            var result = this.orderService.GetAllOrdersByUserId(userId);

            Assert.Equal(2, result.Count);
            Assert.All(result, order => Assert.Equal(userId, order.UserId));
        }

        [Fact]
        public void GetCountByUserIdShouldReturnCorrectQuantitySumWhenOrdersExistInCart()
        {
            // Arrange
            var userId = "User1";
            var orders = new List<Order>
    {
        new Order { UserId = userId, Status = "Cart", Quantity = 2 },
        new Order { UserId = userId, Status = "Cart", Quantity = 3 },
        new Order { UserId = userId, Status = "Done", Quantity = 1 },
    };

            this.orderRepositoryMock.Setup(x => x.AllAsNoTracking())
                .Returns(orders.AsQueryable());

            var result = this.orderService.GetCountByUserId(userId);

            Assert.Equal(5, result);
        }

        [Fact]
        public void GetCurrentOrderShouldReturnCorrectOrderWhenOrderExistsForUserAndProduct()
        {
            // Arrange
            var userId = "User1";
            var productId = "Product1";
            var orders = new List<Order>
    {
        new Order { Id = "Product1", UserId = userId },
        new Order { Id = "Product2", UserId = userId },
    };

            this.orderRepositoryMock.Setup(x => x.All())
                .Returns(orders.AsQueryable());

            // Act
            var result = this.orderService.GetCurrentOrder(userId, productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public void GetTotalPriceShouldReturnCorrectTotalPriceWhenOrdersExist()
        {
            var orders = new List<OrdersInListViewModel>
             {
                 new OrdersInListViewModel { Price = 100, Quantity = 2 },  // Total: 200
                 new OrdersInListViewModel { Price = 50, Quantity = 3 },   // Total: 150
             };

            var result = this.orderService.GetTotalPrice(orders);

            Assert.Equal(375, result); // 200 + 150 + 25 = 375
        }

        [Fact]
        public void GenerateOrderNumberShouldReturnUniqueOrderNumber()
        {
            var orderNumber1 = this.orderService.GenerateOrderNumber();
            var orderNumber2 = this.orderService.GenerateOrderNumber();

            Assert.StartsWith("COM-", orderNumber1);
            Assert.StartsWith("COM-", orderNumber2);
            Assert.NotEqual(orderNumber1, orderNumber2); // Ensure they are unique
        }

        [Fact]
        public async Task UpdateAsyncShouldUpdateOrderQuantitiesWhenOrdersExist()
        {
            var userId = "User1";
            var updateOrderInput = new UpdateOrderInputModel
            {
                UserId = userId,
                Id = ["Order1", "Order2"],
                Quantity = [5, 10],
            };

            var orders = new List<Order>
               {
                 new Order { Id = "Order1", UserId = userId, Quantity = 2 },
                 new Order { Id = "Order2", UserId = userId, Quantity = 3 },
               };

            var orderRepoMock = new Mock<IDeletableEntityRepository<Order>>();
            orderRepoMock.Setup(x => x.All()).Returns(orders.AsQueryable());

            var orderService = new OrderService(orderRepoMock.Object, Mock.Of<IAddressService>());

            await orderService.UpdateAsync(updateOrderInput);

            var updatedOrder1 = orders.First(o => o.Id == "Order1");
            var updatedOrder2 = orders.First(o => o.Id == "Order2");

            Assert.Equal(5, updatedOrder1.Quantity);
            Assert.Equal(10, updatedOrder2.Quantity);

            orderRepoMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
