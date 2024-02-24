namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    public class BaseNewsViewModel
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
    }
}
