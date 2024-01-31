﻿namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Web.ViewModels;

    public interface INewsService
    {
        Task CreateAsync(CreateNewsInputModel input, string userId, string imagePath);

        IEnumerable<NewsInListViewModel> GetAll(int page, int itemsPerPage = 6);

        int GetCount();

        T GetById<T>(int id);
    }
}
