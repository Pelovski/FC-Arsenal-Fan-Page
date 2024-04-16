namespace FCArsenalFanPage.Data.Models
{
    using System;

    using FCArsenalFanPage.Data.Common.Models;

    public class Adress : BaseDeletableModel<string>
    {
        public Adress()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
