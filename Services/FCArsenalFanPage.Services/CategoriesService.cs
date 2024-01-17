namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<SelectListItem> GetAll()
        {
           return this.categoriesRepository.All().Select(x => new SelectListItem
           {
               Value = x.Id.ToString(),
               Text = x.Name,
           }).ToList();
        }
    }
}
