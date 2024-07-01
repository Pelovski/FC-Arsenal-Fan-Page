namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class OrderStatusViewModel
    {
        public string AddressId { get; set; }

        public string PaymentMethod { get; set; }

        public IEnumerable<OrdersInListViewModel> Orders { get; set; }
    }
}
