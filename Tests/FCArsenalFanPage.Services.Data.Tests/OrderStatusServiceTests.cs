namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class OrderStatusServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateOrderStatusCorrectly()
        {
            // Arrange
            var mockOrderService = new Mock<IOrderService>();
            var mockOrderStatusRepository = new Mock<IDeletableEntityRepository<OrderStatus>>();

            var orders = new List<OrdersInListViewModel>
        {
            new OrdersInListViewModel
            {
                UserId = "User1",
            },
        };

            mockOrderService.Setup(o => o.GetTotalPrice(It.IsAny<IEnumerable<OrdersInListViewModel>>())).Returns(100);
            mockOrderService.Setup(o => o.GetAllOrdersByUserId(It.IsAny<string>())).Returns(new List<Order>());
            mockOrderService.Setup(o => o.GenerateOrderNumber()).Returns("12345");

            var service = new OrderStatusService(mockOrderStatusRepository.Object, mockOrderService.Object);

            // Act
            await service.CreateAsync("Address1", "CreditCard", orders);

            // Assert
            mockOrderStatusRepository.Verify(r => r.AddAsync(It.IsAny<OrderStatus>()), Times.Once);
            mockOrderStatusRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
