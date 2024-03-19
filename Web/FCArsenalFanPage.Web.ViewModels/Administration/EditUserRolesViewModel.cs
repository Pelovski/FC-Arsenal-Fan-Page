namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditUserRolesViewModel
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
