namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface IRoleService
    {
        IEnumerable<SelectListItem> GetAll();

        Task UpdateAsync(string userId, string newRole);
    }
}
