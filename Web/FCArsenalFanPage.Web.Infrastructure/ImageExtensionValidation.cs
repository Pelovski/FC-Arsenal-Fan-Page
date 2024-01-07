namespace FCArsenalFanPage.Web.Infrastructure
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    using Microsoft.AspNetCore.Http;

    public class ImageExtensionValidation : ValidationAttribute
    {
        private readonly string[] extensions;

        public ImageExtensionValidation(string[] extensions)
        {
            this.extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (!this.extensions.Contains(extension))
                {
                    return new ValidationResult(this.GetErrorMessage(extension));
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage(string fileExtension)
        {
            // Concatenate the allowed file extensions into a formatted string
            // Example output: "PNG, JPG, GIF or PDF"

            var allowedExtensions = string.Join(", ", this.extensions
                .Take(this.extensions.Length - 1)
                .Select(e => e.TrimStart('.')
                .ToUpper())) + " or " + this.extensions
                .Last()
                .TrimStart('.')
                .ToUpper();

            return $"The photo extension ({fileExtension}) is not allowed! Please use one of the following formats: {allowedExtensions}";
        }
    }
}
