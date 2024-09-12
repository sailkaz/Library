using Library.WebAPI.Entities;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string lastName);
        Task<Author> GetAuthorAsync(int id, string lastName);
        Task<Author> GetAuthorByIdAsync(int id);
        void AddAuthor(Author newAuthor);
        Task AddAuthorForBookAsync(int bookId, Author authorToAdd);
        void UpdateAuthor();
        void DeleteAuthor(Author authorToRemove);
        Task<bool> BookExists(int bookId);
    }
}
