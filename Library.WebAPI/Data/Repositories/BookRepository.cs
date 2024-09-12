using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ILibraryDbContext _context;

        public BookRepository(ILibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> GetBooksByPartOfTitleAsync(string partOfTitle, bool includeAuthors)
        {
            if(includeAuthors)
            {
                return await _context.Books
                .Include(x => x.Authors)
                .Where(x => x.Title.Contains(partOfTitle))
                .ToListAsync();
            }
            
            return await _context.Books
                .Where(x => x.Title.Contains(partOfTitle))
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }

        public void DeleteBook(Book bookToRemove)
        {
            _context.Books.Remove(bookToRemove);
        }

        public async Task<bool> BookExistsAsync(int bookId)
        {
            return await _context.Books.AnyAsync(x => x.Id == bookId);
        }
    }
}