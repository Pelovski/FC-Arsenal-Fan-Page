namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateNewsInputModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MinLength(10, ErrorMessage = $"{nameof(Title)} cannot be less than 10 characters")]
        [MaxLength(100, ErrorMessage = $"{nameof(Title)} cannot exceed 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = $"{nameof(Content)} is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Categories are required")]
        [Display(Name = "Categories")]
        public int CategoryId { get; set; }

        public string CreatedByUserId { get; set; }

        // TODO: Make custom validation attribute for extensions and size
        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }
    }
}
