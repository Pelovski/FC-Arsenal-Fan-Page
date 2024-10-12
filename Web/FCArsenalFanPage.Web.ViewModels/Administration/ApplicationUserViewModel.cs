namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System;
    using System.ComponentModel;

    public class ApplicationUserViewModel
    {
        public string UserId { get; set; }

        [DisplayName("Username")]
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Role { get; set; }
    }
}
