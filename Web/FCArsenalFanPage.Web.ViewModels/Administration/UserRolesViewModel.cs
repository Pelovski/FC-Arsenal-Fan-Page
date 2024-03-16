namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class UserRolesViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
