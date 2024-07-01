namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Models;

    public class CheckoutViewModel : BaseOrderViewModel
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<OrdersInListViewModel> Orders { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
