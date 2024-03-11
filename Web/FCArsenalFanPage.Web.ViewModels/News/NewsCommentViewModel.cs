namespace FCArsenalFanPage.Web.ViewModels.News
{
    using System;
    using AutoMapper;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using Ganss.Xss;

    public class NewsCommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string? UserImageUrl { get; set; }

        public string UserUserName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, NewsCommentViewModel>()
                .ForMember(x => x.UserImageUrl, opt =>
                opt.MapFrom(x => x.User.ProfilePicture.RemoteImageUrl ?? "/Images/ProfilePictures/" + x.User.ProfilePictureId + "." + x.User.ProfilePicture.Extension));
        }
    }
}
