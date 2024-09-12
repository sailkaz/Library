using Library.WebAPI.Entities;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IRentService
    {
        Task<bool> BookExists(int id);
        Task<bool> CheckBookStatus(int id);
        Task StartRentAsync(Rent rentToAdd, int bookId);
    }
}