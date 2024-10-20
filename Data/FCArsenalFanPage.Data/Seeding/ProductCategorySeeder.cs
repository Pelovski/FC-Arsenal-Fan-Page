namespace FCArsenalFanPage.Data.Seeding
{
    using System;
	using System.Linq;
	using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;

    public class ProductCategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ProductCategories.Any())
            {
                return;
            }

            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Sale" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Kit" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Training" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Mens" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Womens" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Kids" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "For Everyone" });
            await dbContext.ProductCategories.AddAsync(new ProductCategory { Name = "Accessories" });
        }
    }
}
