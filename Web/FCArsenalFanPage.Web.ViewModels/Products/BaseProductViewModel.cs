namespace FCArsenalFanPage.Web.ViewModels.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class BaseProductViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter the name of the product.")]
        [MinLength(5, ErrorMessage = $"{nameof(Name)} cannot be less than 5 characters.")]
        [MaxLength(50, ErrorMessage = $"{nameof(Name)} cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Range(1, short.MaxValue, ErrorMessage = "The price should be between $1 and $32,767.")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = $"The quantity should be between 1 and 2147483647")]
        public int Quantity { get; set; }

        [MinLength(20, ErrorMessage = $"{nameof(Description)} cannot be less than 20 characters.")]
        [Required(ErrorMessage = $"{nameof(Description)} is required.")]
        public string Description { get; set; }

        [Display(Name = "Product Category")]
        public int ProductCategoryId { get; set; }

        public string CreatedByUserId { get; set; }

        public IEnumerable<SelectListItem> ProductsItems { get; set; }
    }
}
