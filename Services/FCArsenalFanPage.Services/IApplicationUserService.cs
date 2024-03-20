namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Http;

    public interface IApplicationUserService
    {
        Task SetProfilePictureAsync(IFormFile profilePicture, ApplicationUser user, string imagePath);

        string GetProfilePictureUrl(ApplicationUser user);

        IEnumerable<UserRolesViewModel> GetAllUsersWithRole();
    }
}
