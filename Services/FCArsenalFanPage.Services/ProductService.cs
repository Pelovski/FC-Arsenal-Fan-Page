namespace FCArsenalFanPage.Services
{
	using System.IO;
	using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(
            IDeletableEntityRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task CreateAsync(CreateProductInputModel input, string userId, string imagePath)
        {

            Directory.CreateDirectory($"{imagePath}/Products/");

            var product = new Product
            {
                Id = input.Id,
                Name = input.Name,
                Price = input.Price,
                Quantity = input.Quantity,
                Description = input.Description,
                ProductCategoryId = input.ProductCategoryId,
            };

            var image = input.Image;

            var imageExtension = Path.GetExtension(image.FileName).TrimStart('.');

            product.Image = new Image
            {
                Product = product,
                Extension = imageExtension,
            };

            var imageId = product.Image.Id;

            var physicalPath = $"{imagePath}/Products/{imageId}.{imageExtension}";

            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }
    }
}
