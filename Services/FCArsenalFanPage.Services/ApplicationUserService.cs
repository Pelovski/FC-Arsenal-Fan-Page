namespace FCArsenalFanPage.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public ApplicationUserService(
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task SetProfilePictureAsync(IFormFile profilePicture, ApplicationUser user, string imagePath)
        {
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

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
