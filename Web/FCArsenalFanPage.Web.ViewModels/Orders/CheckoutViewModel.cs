namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CheckoutViewModel
    {
        public string UserId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Street { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public int PostalCode { get; set; }

        public double TotalPrice { get; set; }

        public IEnumerable<OrdersInListViewModel> Orders { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public IEnumerable<Adress> Adresses { get; set; }
    }
}
