namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FCArsenalFanPage.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateNewsInputModel
    {
        [Required(ErrorMessage = "Please enter a title.")]
        [MinLength(10, ErrorMessage = $"{nameof(Title)} cannot be less than 10 characters.")]
        [MaxLength(100, ErrorMessage = $"{nameof(Title)} cannot exceed 100 characters.")]
        public string Title { get; set; }

        [MinLength(200, ErrorMessage = $"{nameof(Content)} cannot be less than 200 characters.")]
        [Required(ErrorMessage = $"{nameof(Content)} is required.")]
        public string Content { get; set; }

        [Display(Name = "Categories")]
        public int CategoryId { get; set; }

        public string CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        [ImageMaxFileSize(10 * 1024 * 1024)]
        [ImageExtensionValidation(new string[] { ".jpg", ".gif", ".png" })]
        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }
    }
}
