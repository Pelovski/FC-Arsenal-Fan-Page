namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;

    public interface IAdressService
    {
        Task<Adress> AddUniqueAddressAsync(string street, string country, string city, int postalCode);

        ICollection<Adress> GetAdressesByUser(ApplicationUser user);
    }
}
