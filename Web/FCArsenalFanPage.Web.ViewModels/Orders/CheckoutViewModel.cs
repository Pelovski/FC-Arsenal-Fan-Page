﻿namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FCArsenalFanPage.Data.Models;

    public class CheckoutViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int PostalCode { get; set; }

        public double TotalPrice { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<OrdersInListViewModel> Orders { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
