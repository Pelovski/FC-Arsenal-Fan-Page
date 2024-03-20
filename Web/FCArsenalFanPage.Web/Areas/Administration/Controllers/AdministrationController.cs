namespace FCArsenalFanPage.Web.Areas.Administration.Controllers
{
    using FCArsenalFanPage.Common;
    using FCArsenalFanPage.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.AssistantAdministrator}")]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
