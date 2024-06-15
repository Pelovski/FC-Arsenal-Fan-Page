namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;

    public class AdressService : IAdressService
    {
        private readonly IDeletableEntityRepository<Adress> adressRepository;

        public AdressService(
            IDeletableEntityRepository<Adress> adressRepository)
        {
            this.adressRepository = adressRepository;
        }

        public async Task<Adress> AddUniqueAddressAsync(string street, string country, string city, int postalCode)
        {

            var existingAddress = this.adressRepository.All()
                .FirstOrDefault(a => a.Name == street &&
                a.Country == country &&
                a.City == city &&
                a.PostalCode == postalCode);

            if (existingAddress != null)
            {
                return existingAddress;
            }

            var newAdress = new Adress
            {
               Name = street,
               Country = country,
               City = city,
               PostalCode = postalCode,
            };

            await this.adressRepository.AddAsync(newAdress);
            await this.adressRepository.SaveChangesAsync();

            return newAdress;
        }

        public ICollection<Adress> GetAdressesByUser(ApplicationUser user)
        {
            return this.adressRepository
                .AllAsNoTracking()
                .Where(a => a.UserId == user.Id)
                .ToList();
        }
    }
}
