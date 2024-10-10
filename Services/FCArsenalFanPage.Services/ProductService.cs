namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using FCArsenalFanPage.Web.ViewModels.Products;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository)
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

        public IEnumerable<ProductInListViewModel> GetAllWithPaging(int page, int itemsPerPage = 6)
        {
            return this.GetAll()
                   .Skip((page - 1) * itemsPerPage)
                   .Take(itemsPerPage);
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
                    ImageUrl = x.Image.RemoteImageUrl ?? "/Images/Products/" + x.Image.Id + "." + x.Image.Extension,
                }).ToList();
        }

        public T GetById<T>(string id)
        {
            var product = this.productRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return product;
        }

        public async Task UpdateAsync(string id, EditProductInputViewModel input)
        {
            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);

            product.Name = input.Name;
            product.Price = input.Price;
            product.Quantity = input.Quantity;
            product.Description = input.Description;
            product.ProductCategoryId = input.ProductCategoryId;
            product.CreatedByUserId = input.CreatedByUserId;

            await this.productRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.productRepository.AllAsNoTracking().Count();
        }
    }
}
