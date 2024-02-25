namespace FCArsenalFanPage.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FCArsenalFanPage.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateProductInputModel : BaseProductViewModel
    {
        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        [ImageMaxFileSize(10 * 1024 * 1024)]
        [ImageExtensionValidation(new string[] { ".jpg", ".gif", ".png" })]
        public IFormFile Image { get; set; }
    }
}
