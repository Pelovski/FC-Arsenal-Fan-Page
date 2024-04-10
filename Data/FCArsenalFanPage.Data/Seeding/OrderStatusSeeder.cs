namespace FCArsenalFanPage.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;

    public class OrderStatusSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.OrderStatuses.Any())
            {
                return;
            }

            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = "Active" });
            await dbContext.OrderStatuses.AddAsync(new OrderStatus { Name = "Completed" });

        }
    }
}
