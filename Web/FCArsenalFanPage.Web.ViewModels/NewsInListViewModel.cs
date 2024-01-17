﻿namespace FCArsenalFanPage.Web.ViewModels
{
    using AutoMapper;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;

    public class NewsInListViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public int CategoryId { get; set; }

        public string CreatedOn { get; set; }

        public string Content { get; set; }

        public string Details { get; set; }
    }
}
