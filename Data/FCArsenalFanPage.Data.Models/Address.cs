﻿namespace FCArsenalFanPage.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FCArsenalFanPage.Data.Common.Models;

    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Country { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        public int PostalCode { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
