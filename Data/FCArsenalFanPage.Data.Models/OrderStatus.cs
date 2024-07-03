namespace FCArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using FCArsenalFanPage.Data.Common.Models;

    public class OrderStatus : BaseDeletableModel<int>
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }

        public string PaymentMethod { get; set; }

        public string OrderNumber { get; set; }

        public double TotalPrice { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
