namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System;

    public class UserRolesViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Role { get; set; }
    }
}
