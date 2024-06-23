﻿namespace FCArsenalFanPage.Services
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

        string GetProfilePictureUrl(ApplicationUser user);

        IEnumerable<UserRolesViewModel> GetAllUsersWithRole();

        Task SetNameToUserAsync(ApplicationUser user, string name);

        Task<bool> SetAdressToUserAsync(ApplicationUser user, string street, string country, string city, int postalCode);
    }
}
