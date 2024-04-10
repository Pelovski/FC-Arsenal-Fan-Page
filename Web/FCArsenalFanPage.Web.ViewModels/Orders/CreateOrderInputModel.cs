namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using AutoMapper;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;
    using FCArsenalFanPage.Web.ViewModels.Products;

    public class CreateOrderInputModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, CreateOrderInputModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x => x.Image.RemoteImageUrl != null ?
                  x.Image.RemoteImageUrl :
                  "/Images/Products/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
