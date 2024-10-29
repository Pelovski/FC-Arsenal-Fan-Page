namespace FCArsenalFanPage.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using FCArsenalFanPage.Web.ViewModels.Administration;
    using FCArsenalFanPage.Web.ViewModels.Products;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Order> orderRepository;

        public ProductService(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Order> orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
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
                CreatedByUserId = userId,
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

        public async Task UpdateProductQuantityAsync(string productId, int quantity, bool isAdding)
        {
            var currentProduct = this.productRepository
                .All()
                .FirstOrDefault(x => x.Id == productId);

            if (quantity > 0)
            {
                if (currentProduct.Quantity >= quantity && !isAdding)
                {
                    currentProduct.Quantity -= quantity;
                }
                else
                {
                    if (isAdding)
                    {
                        currentProduct.Quantity += quantity;
                    }
                }
            }

            this.productRepository.Update(currentProduct);
            await this.productRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.productRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<ProductDashboardViewModel> GetProductsForDashboard()
        {
            return this.productRepository
                .AllAsNoTracking()
                .Include(p => p.CreatedByUser)
                .Include(p => p.ProductCategory)
                .Select(x => new ProductDashboardViewModel
                {
                    ProductId = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    Quantity = x.Quantity,
                    ProductCategory = x.ProductCategory.Name,
                    CreatedByUser = x.CreatedByUser.Name,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    ModifiedOn = x.ModifiedOn != null ? x.ModifiedOn.Value.ToString("dd/MM/yyyy") : string.Empty,
                })
                .ToList();
        }

        public async Task DeleteAsync(string id)
        {
            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                var ordersWithCurrentProduct = this.orderRepository
                    .All()
                    .Where(x => x.ProductId == id)
                    .ToList();

                foreach (var order in ordersWithCurrentProduct)
                {
                    this.orderRepository.HardDelete(order);
                }

                this.productRepository.Delete(product);
            }

            await this.productRepository.SaveChangesAsync();
            await this.orderRepository?.SaveChangesAsync();
        }
    }
}
