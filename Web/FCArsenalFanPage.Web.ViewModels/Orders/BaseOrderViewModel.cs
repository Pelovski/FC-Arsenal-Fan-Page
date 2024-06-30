namespace FCArsenalFanPage.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;

    public class BaseOrderViewModel
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
    }
}
