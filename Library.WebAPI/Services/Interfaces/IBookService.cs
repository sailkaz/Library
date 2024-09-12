using Library.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksByPartOfTitleAsync(string partOfTitle, bool includeAuthors);
        Task<Book> GetBookByIdAsync(int bookId);
        Task AddBookForAuthorAsync(int authorId, Book bookToAdd);
        void UpdateBook();
        void DeleteBook(Book bookToRemove);
        Task<bool> AuthorExistsAsync(int authorId);
    }
}
