using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class BookRentsRepository : IBookRentsRepository
    {
        private readonly ILibraryDbContext _context;

        public BookRentsRepository(ILibraryDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BookRent?> GetBookRentAsync(int bookId)
        {
            var bookRent = await _context.BookRents
                .Include(x => x.Rent.Books)
                .FirstOrDefaultAsync(x => x.BookId == bookId);
            return bookRent;
        }
    }
}
