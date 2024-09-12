using Library.WebAPI.Data;
using Library.WebAPI.Entities;
using Library.WebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<IEnumerable<Book>> GetBooksByPartOfTitleAsync(string partOfTitle, bool includeAuthors)
        {
            return await _unitOfWork.BookRepository.GetBooksByPartOfTitleAsync(partOfTitle, includeAuthors);
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _unitOfWork.BookRepository.GetBookByIdAsync(bookId);
        }

        public async Task AddBookForAuthorAsync(int authorId, Book bookToAdd)
        {
            var author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(authorId);
            author.Books.Add(bookToAdd);
            _unitOfWork.Complete();
        }

        public void UpdateBook()
        {
            _unitOfWork.Complete();
        }

        public void DeleteBook(Book bookToRemove)
        {
            _unitOfWork.BookRepository.DeleteBook(bookToRemove);
            _unitOfWork.Complete();
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await _unitOfWork.AuthorRepository.AuthorExistsAsync(authorId);
        }
    }

}
