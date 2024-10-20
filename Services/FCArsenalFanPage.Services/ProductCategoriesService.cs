namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IDeletableEntityRepository<ProductCategory> productCategoryRepository;

        public ProductCategoriesService(
            IDeletableEntityRepository<ProductCategory> productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public IEnumerable<SelectListItem> GetAll()
        {
            return this.productCategoryRepository
                .All()
                .Where(x => x.Name != "Any")
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name.ToUpper(),
                }).OrderBy(x => x.Text)
                  .ToList();
        }
    }
}
