using Library.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooksByPartOfTitleAsync(string partOfTitle, bool includeAuthors);
        Task<Book> GetBookByIdAsync(int bookId);
        void DeleteBook(Book bookToRemove);
        Task<bool> BookExistsAsync(int bookId);
    }
}
