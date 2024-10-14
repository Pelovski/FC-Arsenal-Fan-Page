namespace FCArsenalFanPage.Web.ViewModels.Products
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Services.Mapping;

    public class SingleProductViewModel : IMapFrom<Product>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        public int AvailableQuantity { get; set; }

        public string ProductCategoryName { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, SingleProductViewModel>()
                 .ForMember(x => x.ImageUrl, opt =>
                  opt.MapFrom(x => x.Image.RemoteImageUrl ?? "/Images/Products/" + x.Image.Id + "." + x.Image.Extension));
        }
    }
}
