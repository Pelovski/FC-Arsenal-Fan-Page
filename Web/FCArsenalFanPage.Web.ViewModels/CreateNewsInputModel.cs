namespace FCArsenalFanPage.Web.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Microsoft.AspNetCore.Http;

    public class CreateNewsInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string CreatedByUserId { get; set; }

        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
