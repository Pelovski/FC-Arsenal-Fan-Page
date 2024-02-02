namespace FCArsenalFanPage.Services
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
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

        public IEnumerable<ProductInListViewModel> GetAll()
        {
           return this.productRepository
                .AllAsNoTracking()
                .Select(x => new ProductInListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCategoryName = x.ProductCategory.Name,
                    ImageUrl = x.Image.RemoteImageUrl != null ?
                               x.Image.RemoteImageUrl :
                              "/Images/Products/" + x.Image.Id + "." + x.Image.Extension,
                }).ToList();
        }
    }
}
