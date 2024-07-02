namespace FCArsenalFanPage.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FCArsenalFanPage.Data.Models;
    using FCArsenalFanPage.Web.ViewModels.Orders;

    public interface IOrderStatusService
    {
        Task CreateAsync(string addressId, string paymentMethod, string userId);
    }
}
