namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using Ganss.Xss;

    public class SingleNewsViewModel : IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<NewsInListViewModel> RecentPosts { get; set; }

        public IEnumerable<NewsCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, SingleNewsViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x => x.Image.RemoteImageUrl ?? "/Images/News/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
