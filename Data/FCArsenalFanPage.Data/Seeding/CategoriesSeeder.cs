namespace FCArsenalFanPage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Any" });
            await dbContext.Categories.AddAsync(new Category { Name = "History" });
            await dbContext.Categories.AddAsync(new Category { Name = "Academy" });
            await dbContext.Categories.AddAsync(new Category { Name = "Club" });
            await dbContext.Categories.AddAsync(new Category { Name = "Women" });
            await dbContext.Categories.AddAsync(new Category { Name = "Men" });

        }
    }
}
