namespace FCArsenalFanPage.Web.ViewModels.Products
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductListViewModel : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

    }
}
