namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using FCArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAdressService adressService;

        public ApplicationUserService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Image> imageRepository,
            UserManager<ApplicationUser> userManager,
            IAdressService adressService)
        {
            this.usersRepository = usersRepository;
            this.imageRepository = imageRepository;
            this.userManager = userManager;
            this.adressService = adressService;
        }

        public async Task SetProfilePictureAsync(IFormFile profilePicture, ApplicationUser user, string imagePath)
        {
            if (user.ProfilePictureId != null)
            {
                var currentProfilePicture = this.imageRepository.All().FirstOrDefault(x => user.ProfilePictureId == x.Id);

                var currentImagePath = $"{imagePath}/ProfilePictures/{currentProfilePicture.Id}.{currentProfilePicture.Extension}";

                File.Delete(currentImagePath);
            }

            Directory.CreateDirectory($"{imagePath}/ProfilePictures/");

            var imageExtension = Path.GetExtension(profilePicture.FileName).TrimStart('.');

            user.ProfilePicture = new Image
            {
                User = user,
                Extension = imageExtension,
            };

            var imageId = user.ProfilePicture.Id;

            var physicalPath = $"{imagePath}/ProfilePictures/{imageId}.{imageExtension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await profilePicture.CopyToAsync(fileStream);

            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public string GetProfilePictureUrl(ApplicationUser user)
        {
            var profilePicture = this.imageRepository.All().FirstOrDefault(x => user.ProfilePictureId == x.Id);

            if (profilePicture == null)
            {
                return "/Images/ProfilePictures/noImage.png";
            }

            return profilePicture.RemoteImageUrl ??
                "/Images/ProfilePictures/" + profilePicture.Id + "." + profilePicture.Extension;
        }

        public IEnumerable<UserRolesViewModel> GetAllUsersWithRole()
        {
            var users = this.userManager.Users.ToList();

            return users
                .SelectMany(user => this.userManager.GetRolesAsync(user)
                    .Result
                    .Select(role => new UserRolesViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Role = role,
                        CreatedOn = user.CreatedOn,
                        Email = user.Email,
                    }))
                .ToList();
        }

        public async Task SetNameToUserAsync(ApplicationUser user, string name)
        {
            user.Name = name;
            this.usersRepository.Update(user);
            await this.usersRepository.SaveChangesAsync();
        }

        public async Task SetAdressToUserAsync(ApplicationUser user, string street, string country, string city, int postalCode)
        {
            var adress = await this.adressService.AddUniqueAddressAsync(street, country, city, postalCode);

            if (user.Adresses.FirstOrDefault(a => a.Id == adress.Id) == null)
            {
                user.Adresses.Add(adress);
                this.usersRepository.Update(user);
                await this.usersRepository.SaveChangesAsync();
            }
        }
    }
}
