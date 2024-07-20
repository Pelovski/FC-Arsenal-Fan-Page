namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class MyOrderViewModel
    {
        public string OrderNumber { get; set; }

        public string CreatedOn { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        public IEnumerable<OrdersInListViewModel> Orders { get; set; }
    }
}
