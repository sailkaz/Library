using Library.WebAPI.Data;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;

namespace Library.WebAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _unitOfWork.AuthorRepository.GetAuthorsAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string lastName)
        {
            return await _unitOfWork.AuthorRepository.GetAuthorsByNameAsync(lastName);
        }

        public async Task<Author> GetAuthorAsync(int id, string lastName)
        {
            return await _unitOfWork.AuthorRepository.GetAuthorAsync(id, lastName);
        }
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
        }

        public void AddAuthor(Author newAuthor)
        {
            _unitOfWork.AuthorRepository.AddAuthor(newAuthor);
            _unitOfWork.Complete();
        }

        public async Task AddAuthorForBookAsync(int bookId, Author authorToAdd)
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
            book.Authors.Add(authorToAdd);
            _unitOfWork.Complete();
        }

        public void UpdateAuthor()
        {
            _unitOfWork.Complete();
        }

        public void DeleteAuthor(Author authorToRemove)
        {
            _unitOfWork.AuthorRepository.DeleteAuthor(authorToRemove);
            _unitOfWork.Complete();
        }

        public async Task<bool> BookExists(int bookId)
        {
            return await _unitOfWork.BookRepository.BookExistsAsync(bookId); ;
        }

    }
}
