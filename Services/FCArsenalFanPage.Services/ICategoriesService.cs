using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FCArsenalFanPage.Services
{
    public interface ICategoriesService
    {
       IEnumerable<SelectListItem> GetAll();
    }
}
