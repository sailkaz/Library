using Library.WebAPI.Entities;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface IBookRentsRepository
    {
        Task<BookRent?> GetBookRentAsync(int bookId);
    }
}
