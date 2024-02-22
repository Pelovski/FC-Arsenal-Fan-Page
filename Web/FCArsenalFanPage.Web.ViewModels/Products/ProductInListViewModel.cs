namespace FCArsenalFanPage.Web.ViewModels.Products
{
    public class ProductInListViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }

        public string ProductCategoryName { get; set; }
    }
}
