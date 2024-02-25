namespace FCArsenalFanPage.Web.ViewModels.News
{
    using AutoMapper;

    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;

    public class EditNewsInputViewModel : BaseNewsViewModel, IMapFrom<News>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<News, EditNewsInputViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x => x.Image.RemoteImageUrl ?? "/Images/News/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
