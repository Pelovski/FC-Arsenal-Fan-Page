namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        private Mock<IDeletableEntityRepository<Address>> mockRepo;
        private AddressService service;

        public AddressServiceTests()
        {
            this.mockRepo = new Mock<IDeletableEntityRepository<Address>>();
            this.service = new AddressService(this.mockRepo.Object);
        }

        [Fact]
        public async Task AddUniqueAddressAsyncShouldAddNewAddressWhenNoExistingAddress()
        {
            var list = new List<Address>();

            this.mockRepo
                .Setup(x => x.All())
                .Returns(list.AsQueryable());

            this.mockRepo
                .Setup(x => x.AddAsync(It.IsAny<Address>()))
                .Callback((Address address) => list.Add(address));

            var user = new ApplicationUser();
            var street = "123 Test St";
            var country = "TestCountry";
            var city = "TestCity";
            var postalCode = 12345;

            var result = await this.service.AddUniqueAddressAsync(user, street, country, city, postalCode);

            this.mockRepo.Verify(x => x.AddAsync(It.IsAny<Address>()), Times.Once);
            this.mockRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
            Assert.Equal(street, result.Name);
            Assert.Equal(country, result.Country);
            Assert.Equal(city, result.City);
            Assert.Equal(postalCode, result.PostalCode);
        }

        [Fact]
        public async Task AddUniqueAddressAsyncShouldReturnExistingAddressWhenAddressExists()
        {
            var user = new ApplicationUser();
            var street = "123 Test St";
            var country = "TestCountry";
            var city = "TestCity";
            var postalCode = 12345;

            var existingAddress = new Address
            {
                Name = street,
                Country = country,
                City = city,
                PostalCode = postalCode,
            };

            var addresses = new List<Address> { existingAddress };
            this.mockRepo.Setup(repo => repo.All())
                .Returns(addresses.AsQueryable());

            var result = await this.service.AddUniqueAddressAsync(user, street, country, city, postalCode);

            this.mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Address>()), Times.Never);
            this.mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Never);
            Assert.Equal(existingAddress, result);
        }

        [Fact]
        public async Task AddUniqueAddressAsyncShouldDeleteOldestAddressWhenUserHasThreeAddresses()
        {
            // Arrange
            var user = new ApplicationUser { Id = "user1" };
            var street = "456 New St";
            var country = "TestCountry";
            var city = "TestCity";
            var postalCode = 67890;

            var addresses = new List<Address>
    {
        new Address { Name = "Address1", Country = "Country1", City = "City1", PostalCode = 11111, UserId = user.Id, CreatedOn = new System.DateTime(2021, 1, 1) },
        new Address { Name = "Address2", Country = "Country2", City = "City2", PostalCode = 22222, UserId = user.Id, CreatedOn = new System.DateTime(2021, 2, 1) },
        new Address { Name = "Address3", Country = "Country3", City = "City3", PostalCode = 33333, UserId = user.Id, CreatedOn = new System.DateTime(2021, 3, 1) },
    };

            this.mockRepo.Setup(repo => repo.AllAsNoTracking())
                .Returns(addresses.AsQueryable().OrderByDescending(a => a.CreatedOn));

            this.mockRepo.Setup(repo => repo.All())
                .Returns(addresses.AsQueryable().OrderByDescending(a => a.CreatedOn));

            Address deletedAddress = null;

            // Capture the address that gets deleted
            this.mockRepo.Setup(repo => repo.Delete(It.IsAny<Address>()))
                .Callback<Address>(addr => deletedAddress = addr);

            // Act
            var result = await this.service.AddUniqueAddressAsync(user, street, country, city, postalCode);

            // Assert: Check which address was actually deleted
            Assert.NotNull(deletedAddress);
            Assert.Equal("Address1", deletedAddress.Name);  // Verify it's the oldest address being deleted

            this.mockRepo.Verify(repo => repo.Delete(It.Is<Address>(a => a.Name == "Address1")), Times.Once);
            this.mockRepo.Verify(repo => repo.AddAsync(It.IsAny<Address>()), Times.Once);
            this.mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
