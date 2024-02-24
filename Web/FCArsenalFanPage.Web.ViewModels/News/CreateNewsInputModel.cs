namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FCArsenalFanPage.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateNewsInputModel : BaseNewsViewModel
    {
        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        [ImageMaxFileSize(10 * 1024 * 1024)]
        [ImageExtensionValidation(new string[] { ".jpg", ".gif", ".png" })]
        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }
    }
}
