namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Diagnostics.Metrics;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using Microsoft.AspNetCore.Http;

    public interface IApplicationUserService
    {
        Task SetProfilePictureAsync(IFormFile profilePicture, ApplicationUser user, string imagePath);

        Task<bool> DeleteApplicationUser(string userId);

        string GetProfilePictureUrl(ApplicationUser user);

        IEnumerable<ApplicationUserViewModel> GetAllUsersWithRole();

        Task SetNameToUserAsync(ApplicationUser user, string name);

        Task<bool> SetAddressToUserAsync(ApplicationUser user, string street, string country, string city, int postalCode);

        public int GetCount();
    }
}
