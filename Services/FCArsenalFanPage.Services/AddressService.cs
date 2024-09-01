namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FCArsenalFanPage.Data.Common.Repositories;
    using FCArsenalFanPage.Data.Models;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;

        public AddressService(
            IDeletableEntityRepository<Address> addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<Address> AddUniqueAddressAsync(ApplicationUser user, string street, string country, string city, int postalCode)
        {

            var addresses = this.GetAddressesByUser(user);

            var existingAddress = this.addressRepository
                .All()
                .FirstOrDefault(a => a.Name == street &&
                a.Country == country &&
                a.City == city &&
                a.PostalCode == postalCode);

            if (existingAddress != null)
            {
                return existingAddress;
            }

            var newAddress = new Address
            {
               Name = street,
               Country = country,
               City = city,
               PostalCode = postalCode,
            };

            if (addresses.Count == 3)
            {
                var oldestAddress = addresses.Last();
                this.addressRepository.Delete(oldestAddress);
            }

            await this.addressRepository.AddAsync(newAddress);
            await this.addressRepository.SaveChangesAsync();

            return newAddress;
        }

        public ICollection<Address> GetAddressesByUser(ApplicationUser user)
        {
            var addresses = this.addressRepository
                  .AllAsNoTracking()
                  .Where(a => a.UserId == user.Id)
                  .OrderByDescending(x => x.CreatedOn)
                  .ToList();

            return addresses;
        }
    }
}
