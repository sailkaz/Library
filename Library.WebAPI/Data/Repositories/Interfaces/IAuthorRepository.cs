using Library.WebAPI.Entities;

namespace Library.WebAPI.Data.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<IEnumerable<Author>> GetAuthorsByNameAsync(string lastName);
        Task<Author> GetAuthorAsync(int id, string lastName);
        Task<Author> GetAuthorByIdAsync(int id);
        void AddAuthor(Author newAuthor);
        void DeleteAuthor(Author authorToRemove);
        Task<bool> AuthorExistsAsync(int authorId);
    }
}
