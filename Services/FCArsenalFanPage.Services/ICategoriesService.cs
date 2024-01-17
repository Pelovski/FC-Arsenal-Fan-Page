namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ICategoriesService
    {
       IEnumerable<SelectListItem> GetAll();
    }
}
