using Library.WebAPI.Entities;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IRentService
    {
        Task<bool> BookExists(int id);
        Task<bool> CheckBookStatus(int id);
        Task<Rent> GetRentAsync(int rentId);
        Task StartRentAsync(Rent rentToAdd);
    }
}