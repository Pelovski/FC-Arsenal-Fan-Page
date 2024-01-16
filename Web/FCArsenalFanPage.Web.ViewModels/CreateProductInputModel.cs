namespace FCArsenalFanPage.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FCArsenalFanPage.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateProductInputModel
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

        [Required(ErrorMessage = "Please select an image.")]
        [DataType(DataType.Upload)]
        [ImageMaxFileSize(10 * 1024 * 1024)]
        [ImageExtensionValidation(new string[] { ".jpg", ".gif", ".png" })]
        public IFormFile Image { get; set; }

        public IEnumerable<SelectListItem> ProductsItems { get; set; }
    }
}
