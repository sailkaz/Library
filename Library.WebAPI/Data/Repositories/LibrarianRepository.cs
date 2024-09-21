using Library.WebAPI.Data.DbContexts;
using Library.WebAPI.Data.Repositories.Interfaces;
using Library.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.WebAPI.Data.Repositories
{
    public class LibrarianRepository : ILibrarianRepository
    {
        private readonly ILibraryDbContext _context;

        public LibrarianRepository(ILibraryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Librarian>> GetLibrariansAsync()
        {
            return await _context.Librarians
                .OrderBy(x => x.LastName)
                .ToListAsync();
        }

        public async Task<Librarian?> GetLibrarianByIdAsync(int id)
        {
            return await _context.Librarians.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Addlibrarian(Librarian newLibrarian)
        {
            _context.Librarians.Add(newLibrarian);
        }

        public void DeleteLibrarian(Librarian librarianToRemove)
        {
            _context.Librarians.Remove(librarianToRemove);
        }
    }
}
