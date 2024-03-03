namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Http;

    public interface IApplicationUserService
    {
        Task SetProfilePictureAsync(IFormFile profilePicture, ApplicationUser user, string imagePath);
    }
}
