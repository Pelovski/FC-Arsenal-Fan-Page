namespace FCArsenalFanPage.Web.ViewModels.Administration
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditUserRolesViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
