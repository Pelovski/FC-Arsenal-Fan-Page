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

        public async Task<Adress> AddUniqueAddressAsync(ApplicationUser user, string street, string country, string city, int postalCode)
        {

            var addresses = this.GetAdressesByUser(user);

            var existingAddress = this.adressRepository
                .All()
                .FirstOrDefault(a => a.Name == street &&
                a.Country == country &&
                a.City == city &&
                a.PostalCode == postalCode);

            if (existingAddress != null)
            {
                return existingAddress;
            }

            var newAddress = new Adress
            {
               Name = street,
               Country = country,
               City = city,
               PostalCode = postalCode,
            };

            if (addresses.Count == 3)
            {
                var firstAddress = addresses.First();
                this.adressRepository.Delete(firstAddress);
            }

            await this.adressRepository.AddAsync(newAddress);
            await this.adressRepository.SaveChangesAsync();

            return newAddress;
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
