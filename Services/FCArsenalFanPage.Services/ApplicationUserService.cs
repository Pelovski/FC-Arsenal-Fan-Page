namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Image> imageRepository;

        public ApplicationUserService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Image> imageRepository)
        {
            this.usersRepository = usersRepository;
            this.imageRepository = imageRepository;
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

            return profilePicture.RemoteImageUrl ??
                "/Images/ProfilePictures/" + profilePicture.Id + "." + profilePicture.Extension;
        }
    }
}
