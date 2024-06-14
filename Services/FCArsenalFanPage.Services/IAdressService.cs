namespace FCArsenalFanPage.Services
{
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Models;

    public interface IAdressService
    {
        Task<Adress> AddUniqueAddressAsync(string street, string country, string city, int postalCode);
    }
}
