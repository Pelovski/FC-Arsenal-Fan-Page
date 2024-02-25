namespace FCArsenalFanPage.Web.ViewModels.News
{
    using FCArsenalFanPage.Data.Models;

    using FCArsenalFanPage.Services.Mapping;

    public class EditNewsInputViewModel : BaseNewsViewModel, IMapFrom<News>
    {
        public int Id { get; set; }
    }
}
