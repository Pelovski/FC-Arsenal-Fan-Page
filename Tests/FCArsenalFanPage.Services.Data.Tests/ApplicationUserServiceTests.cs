namespace FCArsenalFanPage.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;

    public class ApplicationUserServiceTests
    {
        private readonly Mock<IDeletableEntityRepository<ApplicationUser>> mockUsersRepository;
        private readonly Mock<IDeletableEntityRepository<Image>> mockImageRepository;
        private readonly Mock<UserManager<ApplicationUser>> mockUserManager;
        private readonly Mock<IAddressService> mockAddressService;
        private readonly ApplicationUserService userService;

        public ApplicationUserServiceTests()
        {
            this.mockUsersRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            this.mockImageRepository = new Mock<IDeletableEntityRepository<Image>>();
            this.mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            this.mockAddressService = new Mock<IAddressService>();

            this.userService = new ApplicationUserService(
                this.mockUsersRepository.Object,
                this.mockImageRepository.Object,
                this.mockUserManager.Object,
                this.mockAddressService.Object);
        }

        [Fact]
        public async Task SetProfilePictureAsyncShouldUploadProfilePictureWhenUserHasNoCurrentPicture()
        {
            var user = new ApplicationUser { Id = "1", ProfilePictureId = null };
            var mockFormFile = new Mock<IFormFile>();
            var imagePath = "path/to/images";

            mockFormFile.Setup(f => f.FileName).Returns("profile.jpg");
            mockFormFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await this.userService.SetProfilePictureAsync(mockFormFile.Object, user, imagePath);

            this.mockUsersRepository.Verify(repo => repo.Update(user), Times.Once);
            this.mockUsersRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void GetProfilePictureUrlShouldReturnDefaultImageUrlWhenNoProfilePictureExists()
        {
            var user = new ApplicationUser { Id = "1", ProfilePictureId = null };

            var result = this.userService.GetProfilePictureUrl(user);

            Assert.Equal("/Images/ProfilePictures/noImage.png", result);
        }

        [Fact]
        public async Task SetNameToUserAsyncShouldUpdateUserName()
        {
            var user = new ApplicationUser { Id = "1", Name = "OldName" };
            var newName = "NewName";

            // Act
            await this.userService.SetNameToUserAsync(user, newName);

            // Assert
            this.mockUsersRepository.Verify(repo => repo.Update(It.Is<ApplicationUser>(u => u.Name == newName)), Times.Once);
            this.mockUsersRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task SetAddressToUserAsync_ShouldAddAddressToUser()
        {
            var user = new ApplicationUser { Id = "1", Addresses = new List<Address>() };
            var street = "123 Main St";
            var country = "TestCountry";
            var city = "TestCity";
            var postalCode = 12345;

            var newAddress = new Address { Id = "1", Name = street, Country = country, City = city, PostalCode = postalCode };

            this.mockAddressService.Setup(service => service.AddUniqueAddressAsync(user, street, country, city, postalCode))
                .ReturnsAsync(newAddress);

            // Act
            var result = await this.userService.SetAddressToUserAsync(user, street, country, city, postalCode);

            // Assert
            Assert.True(result);
            Assert.Contains(newAddress, user.Addresses);
            this.mockUsersRepository.Verify(repo => repo.Update(user), Times.Once);
            this.mockUsersRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}
