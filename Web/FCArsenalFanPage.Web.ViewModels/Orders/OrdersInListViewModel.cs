namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    public class OrdersInListViewModel
    {
        public string Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Status { get; set; }

        public string UserId { get; set; }
    }
}
