namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;

    public interface IAddressService
    {
        Task<Address> AddUniqueAddressAsync(ApplicationUser user, string street, string country, string city, int postalCode);

        ICollection<Address> GetAddressesByUser(ApplicationUser user);
    }
}
