using Library.WebAPI.Entities;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IRentService
    {
        Task<bool> BookExists(int id);
        Task<bool> CheckBookStatus(int id);
        Task<Book> GetBookByIdAsync(int bookId);
        Task<Rent> GetRentAsync(int rentId);
        Task StartRentAsync(Rent rentToAdd);
        Task CancelRentOfBookAsync(int bookId);
        Task<Rent> GetRentForReaderAsync(int readerId);
    }
}