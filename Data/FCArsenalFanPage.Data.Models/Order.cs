﻿namespace FCArsenalFanPage.Data.Models
{
    using System;

    using FCArsenalFanPage.Data.Common.Models;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Status { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int OrderStatusId { get; set; }

        public virtual OrderStatus OrderDetails { get; set; }
    }
}
