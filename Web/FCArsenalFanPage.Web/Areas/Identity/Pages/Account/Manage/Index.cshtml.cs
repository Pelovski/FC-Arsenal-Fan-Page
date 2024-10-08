﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace FCArsenalFanPage.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
#nullable disable

    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services;
    using FCArsenalFanPage.Web.Infrastructure;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IApplicationUserService applicationUserService;
        private readonly IWebHostEnvironment environment;
        private readonly IAddressService addressService;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IApplicationUserService applicationUserService,
            IWebHostEnvironment environment,
            IAddressService addressService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.applicationUserService = applicationUserService;
            this.environment = environment;
            this.addressService = addressService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First and Last Name")]
            public string Name { get; set; }

            [Required]
            public string UserName { get; set; }

            [DataType(DataType.Upload)]
            [ImageMaxFileSize(10 * 1024 * 1024)]
            [ImageExtensionValidation(new string[] { ".jpg", ".gif", ".png" })]
            public IFormFile ProfilePicture { get; set; }

            [Required]
            public string Street { get; set; }

            [Required]
            public string Country { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public int PostalCode { get; set; }

            public string ImageUrl { get; set; }

            public string UserRole { get; set; }

            public string JoinedOn { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this.userManager.GetUserNameAsync(user);
            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);
            var userRole = await this.userManager.GetRolesAsync(user);
            var test = this.addressService.GetAddressesByUser(user);
            var userAddresses = this.addressService.GetAddressesByUser(user).FirstOrDefault();

            this.Input = new InputModel
            {
                Name = user.Name,
                UserName = userName,
                PhoneNumber = phoneNumber,
                UserRole = userRole.FirstOrDefault(),
                JoinedOn = user.CreatedOn.ToString("dd MMM yyyy"),
            };

            if (userAddresses != null)
            {
                this.Input.Street = userAddresses.Name;
                this.Input.Country = userAddresses.Country;
                this.Input.City = userAddresses.City;
                this.Input.PostalCode = userAddresses.PostalCode;
            }

            if (user.ProfilePictureId != null)
            {
                var imageUrl = this.applicationUserService.GetProfilePictureUrl(user);
                this.Input.ImageUrl = imageUrl;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var imagePath = $"{this.environment.WebRootPath}/Images";

            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this.userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            if (this.Input.Name != user.Name)
            {
                var name = this.Input.Name;
                await this.applicationUserService.SetNameToUserAsync(user, name);
            }

            if (this.Input.UserName != user.UserName)
            {
               var setUserNameResult = await this.userManager.SetUserNameAsync(user, this.Input.UserName);

               if (!setUserNameResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set username.";
                    return this.RedirectToPage();
                }
            }

            var phoneNumber = await this.userManager.GetPhoneNumberAsync(user);

            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            if (this.Input.ProfilePicture != null)
            {
               await this.applicationUserService.SetProfilePictureAsync(this.Input.ProfilePicture, user, imagePath);
            }

            var newAddress = await this.applicationUserService.SetAddressToUserAsync(user, this.Input.Street, this.Input.Country, this.Input.City, this.Input.PostalCode);

            await this.signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";

            return this.RedirectToPage();
        }
    }
}
